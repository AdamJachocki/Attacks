using _6_DataModificationControl.Services;
using _6_DataModificationControl.ViewModels;
using Common.Extensions;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _6_DataModificationControl.Pages
{
    [Authorize]
    public class NewItemModel : PageModel
    {
        [FromRoute]
        public int? Id { get; set; }

        [BindProperty]
        public TodoItemViewModel Item { get; set; }

        readonly InsecureTodoItemService service;

        public NewItemModel(InsecureTodoItemService service)
        {
            this.service = service;
        }
        public async Task OnGet()
        {
            if (Id.HasValue)
                Item = new(await service.GetItem(Id.Value));

            if (Item == null)
                Item = new TodoItemViewModel();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = User.GetId();
            if (!id.HasValue)
                return BadRequest();

            Item.OwnerId = id.Value;

            TodoItem model = Item.ToModel();            
            await service.AddItem(model);
            return RedirectToPage("ListPage");

        }
    }
}
