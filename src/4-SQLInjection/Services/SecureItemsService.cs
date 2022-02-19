using _4_SQLInjection.Data;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace _4_SQLInjection.Services
{
    public class SecureItemsService : IItemsService
    {
        readonly ApplicationDbContext db;

        public SecureItemsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<TodoItem>> GetItems(int userId)
        {
            var query = db.TodoItems.Where(x => x.OwnerId == userId);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> SearchItems(string searchBy, int userId)
        {
            var query = db.TodoItems.Where(x => x.Description.Contains(searchBy)
                && x.OwnerId == userId);
            return await query.ToListAsync();
        }
    }
}
