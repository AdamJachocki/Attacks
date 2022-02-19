using Common.Models;

namespace _4_SQLInjection.Services
{
    public interface IItemsService
    {
        Task<IEnumerable<TodoItem>> SearchItems(string searchBy, int userId);
        Task<IEnumerable<TodoItem>> GetItems(int userId);
    }
}