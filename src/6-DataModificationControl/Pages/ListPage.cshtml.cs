using _6_DataModificationControl.Services;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _6_DataModificationControl.Pages
{
    [Authorize]
    public class ListPageModel : PageModel
    {
        public IEnumerable<TodoItem> Items { get; set; }

        readonly InsecureTodoItemService insecureService;
        readonly SecureTodoItemService secureService;
        readonly UserManager<SystemUser> userMan;

        public ListPageModel(InsecureTodoItemService insecureService, 
            SecureTodoItemService secureService,
            UserManager<SystemUser> userMan
            )
        {
            this.insecureService = insecureService;
            this.secureService = secureService;
            this.userMan = userMan;
        }

        public async Task OnGet()
        {
            SystemUser su = await userMan.GetUserAsync(User);
            Items = await insecureService.GetItems(su.Id);
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            await insecureService.DeleteItem(id);
            //await secureService.DeleteItem(id);
            return RedirectToPage();
        }

        

    }
}
