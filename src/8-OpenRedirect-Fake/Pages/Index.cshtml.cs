using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _8_OpenRedirect_Fake.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public LoginViewModel LoginData { get; set; } = new();

        public async Task<IActionResult> OnPost()
        {
            Console.WriteLine("Nazwa użytkownika banku: " + LoginData.UserName);
            Console.WriteLine("Super tajne hasło: " + LoginData.Password);

            return Redirect("https://localhost:8000");
        }
    }
}