using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _7_CSRF_Main.Pages
{
    public class SecurePageModel : PageModel
    {
        public IActionResult OnPost([FromForm] int id)
        {
            Console.WriteLine($"Usuwam element o id {id}");
            return RedirectToPage();
        }
    }
}
