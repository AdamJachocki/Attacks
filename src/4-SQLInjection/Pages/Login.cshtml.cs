using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _4_SQLInjection.Pages
{
    class LoginPageModel : PageModel
    {
        [BindProperty]
        public LoginViewModel Login { get; set; } = new LoginViewModel();

        SignInManager<SystemUser> signInMan;

        public LoginPageModel(SignInManager<SystemUser> signInMan)
        {
            this.signInMan = signInMan;
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await signInMan.SignOutAsync();
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var result = await signInMan.PasswordSignInAsync(Login.UserName, Login.Password, true, false);
            if (result.Succeeded)
                return RedirectToPage("Items");
            else
                return RedirectToPage();
        }
    }
}
