using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _6_DataModificationControl.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel LoginInfo { get; set; } = new LoginViewModel();

        readonly SignInManager<SystemUser> signInMan;

        public LoginModel(SignInManager<SystemUser> signInMan)
        {
            this.signInMan = signInMan;
        }

        [Authorize]
        public async Task<IActionResult> OnGetLogout()
        {
            await signInMan.SignOutAsync();
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await signInMan.PasswordSignInAsync(LoginInfo.UserName, LoginInfo.Password, true, false);
            if (result.Succeeded)
                return RedirectToPage("/Index");
            else
                return RedirectToPage();
        }
    }
}
