using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _9_PathTraversal.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FileName { get; set; } = string.Empty;

        IWebHostEnvironment webHost;

        public IndexModel(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        public IActionResult OnGetFile()
        {
            if (string.IsNullOrWhiteSpace(FileName))
                return Page();
            string mainPath = Path.Combine(webHost.ContentRootPath, "Data");
            string path = Path.Combine(mainPath, FileName); //podatność

            if (!System.IO.File.Exists(path))
                return NotFound();

            return Content(System.IO.File.ReadAllText(path));
        }

        public IActionResult OnGetFileSecured()
        {
            if (string.IsNullOrWhiteSpace(FileName))
                return Page();

            FileName = Path.GetFileName(FileName); //naprawa podatności
            string mainPath = Path.Combine(webHost.ContentRootPath, "Data");
            string path = Path.Combine(mainPath, FileName);

            if (!System.IO.File.Exists(path))
                return NotFound();

            return Content(System.IO.File.ReadAllText(path));
        }
    }
}