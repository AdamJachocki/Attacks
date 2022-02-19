using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _5_DetailedErrors.Pages
{
    public class BareErrorModel : PageModel
    {
        public string FileContent { get; set; }
        public void OnGet()
        {
            FileContent = System.IO.File.ReadAllText("plik.txt");
        }
    }
}
