using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _5_DetailedErrors.Pages
{
    public class DetailedErrorModel : PageModel
    {
        public string FileContent { get; set; }
        public void OnGet()
        {
            try
            {
                FileContent = System.IO.File.ReadAllText("plik.txt");
            }catch(Exception ex)
            {
                TempData["Error"] = ex;
                throw;
            }
            
        }
    }
}
