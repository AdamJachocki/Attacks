using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2_WithSSL.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string UserLogin { get; set; }
        [BindProperty]
        public string UserPass { get; set; }

        public void OnPost()
        {
            if (!ModelState.IsValid)
                return;

            //akcja logowania
        }
    }
}