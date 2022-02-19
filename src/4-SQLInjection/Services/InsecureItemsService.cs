using _4_SQLInjection.Data;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace _4_SQLInjection.Services
{
    public class InsecureItemsService : IItemsService
    {
        readonly ApplicationDbContext db;

        public InsecureItemsService(ApplicationDbContext db)
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
            string sql = $"SELECT * FROM items where OwnerId = {userId} and Description like '%{searchBy}%'";
            var query = db.TodoItems.FromSqlRaw(sql);
            return await query.ToListAsync();
        }
    }
}
