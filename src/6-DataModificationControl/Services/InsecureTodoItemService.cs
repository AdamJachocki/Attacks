using _6_DataModificationControl.Data;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace _6_DataModificationControl.Services
{
    public class InsecureTodoItemService
    {
        readonly ApplicationDbContext db;

        public InsecureTodoItemService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TodoItem>> GetItems(int userId)
        {
            var query = db.Items.Where(x => x.OwnerId == userId);
            return await query.ToListAsync();
        }

        public async Task<TodoItem> GetItem(int itemId)
        {
            var query = db.Items.Where(x => x.Id == itemId);
            return await query.SingleOrDefaultAsync();
        }

        public async Task AddItem(TodoItem item)
        {
            db.Items.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task DeleteItem(int id)
        {            
            TodoItem item = new TodoItem { Id = id };
            db.Items.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}
