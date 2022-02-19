using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1_NoSSL.Pages
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