using Common.Models;

namespace _6_DataModificationControl.ViewModels
{
    public class TodoItemViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }

        public TodoItemViewModel()
        {

        }

        public TodoItemViewModel(TodoItem item)
        {
            this.Title = item.Title;
            this.Description = item.Description;
            this.OwnerId = item.OwnerId;
        }

        public TodoItem ToModel()
        {
            TodoItem result = new TodoItem();
            result.Title = Title;
            result.Description = Description;
            result.OwnerId = OwnerId;

            return result;
        }
    }
}
