using _4_SQLInjection.Services;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace _4_SQLInjection.Pages
{
    [Authorize]
    public class ItemsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchBy { get; set; } = string.Empty;

        public List<TodoItem> TodoItems { get; set; } = new();


        readonly InsecureItemsService insecureService;
        readonly SecureItemsService secureService;
        readonly UserManager<SystemUser> userMan;

        public ItemsModel(InsecureItemsService insecureService, 
            SecureItemsService secureService,
            UserManager<SystemUser> userMan)
        {
            this.insecureService = insecureService;
            this.secureService = secureService;
            this.userMan = userMan;
        }

        public async Task OnGet()
        {
            var id = GetLoggedUserId();
            if (id.HasValue)
                TodoItems = new(await secureService.GetItems(id.Value));
        }
        public async Task OnGetSearchInsecure()
        {
            if (string.IsNullOrWhiteSpace(SearchBy))
                return;

            var id = GetLoggedUserId();
            if(id.HasValue)
                TodoItems = new(await insecureService.SearchItems(SearchBy, id.Value));
        }

        public async Task OnGetSearchSecure()
        {
            if (string.IsNullOrWhiteSpace(SearchBy))
                return;

            var id = GetLoggedUserId();
            if (id.HasValue)
                TodoItems = new(await secureService.SearchItems(SearchBy, id.Value));
        }

        int? GetLoggedUserId()
        {
            string idValue = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int id = 0;
            if (int.TryParse(idValue, out id))
                return id;
            else
                return null;
        }
    }
}
