using Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace _8_OpenRedirect_Genuine.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public LoginViewModel LoginData { get; set; } = new();
        public SystemUser LoggedUser { get; set; } = null;

        [FromQuery(Name = "return_url")]
        public string? ReturnUrl { get; set; } = string.Empty;
        
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            //symulacja logowania
            bool loginResult = await LoginUser(LoginData);
            if (loginResult)
                return RedirectUserAfterValidLogin(ReturnUrl);
            else
                return RedirectToPage();
        }

        async Task<bool> LoginUser(LoginViewModel data)
        {
            AuthenticationProperties props = new AuthenticationProperties();
            props.IsPersistent = true;

            var principal = CreateClaimsPrincipal(data.UserName);
            await HttpContext.SignInAsync(principal, props);
            return true;
        }

        ClaimsPrincipal CreateClaimsPrincipal(string userName)
        {
            Claim nameClaim = new Claim(ClaimTypes.NameIdentifier, userName);
            
            ClaimsIdentity identity = new ClaimsIdentity(new List<Claim> { nameClaim }, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }

        IActionResult RedirectUserAfterValidLogin(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToPage("/Index");
            else
                return Redirect(returnUrl);
        }
    }
}