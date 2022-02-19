using _6_DataModificationControl.Abstractions;
using _6_DataModificationControl.Data;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web.Http;

namespace _6_DataModificationControl.Services
{
    public class SecureTodoItemService
    {
        readonly ApplicationDbContext db;
        readonly ILoggedUserProvider loggedUserProvider;

        public SecureTodoItemService(ApplicationDbContext db, ILoggedUserProvider loggedUserProvider)
        {
            this.db = db;
            this.loggedUserProvider = loggedUserProvider;
        }

        public async Task<IEnumerable<TodoItem>> GetItems(int userId)
        {
            if(!await CanGetItemsForUser(userId))
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            var query = db.Items.Where(x => x.OwnerId == userId);
            return await query.ToListAsync();
        }

        async Task<bool> CanGetItemsForUser(int userId)
        {
            SystemUser loggedUser = await loggedUserProvider.GetLoggedUser();
            return (loggedUser.Id == userId || await loggedUserProvider.IsLoggedAdmin());
        }

        public async Task<TodoItem> GetItem(int itemId)
        {
            if (!await CanGetOrDeleteItem(itemId))
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            var query = db.Items.Where(x => x.Id == itemId);
            return await query.SingleOrDefaultAsync();
        }

        async Task<bool> CanGetOrDeleteItem(int itemId)
        {
            if (await loggedUserProvider.IsLoggedAdmin())
                return true;

            int? ownerId = await GetItemOwnerId(itemId);
            if (!ownerId.HasValue)
                return false;

            SystemUser loggedUser = await loggedUserProvider.GetLoggedUser();
            return ownerId.Value == loggedUser.Id;
        }

        async Task<int?> GetItemOwnerId(int itemId)
        {
            var query = from x in db.Items
                        where x.Id == itemId
                        select new { OwnerId = x.OwnerId };

            var data = await query.FirstOrDefaultAsync();
            if (data == null)
                return null;

            return data.OwnerId;
        }

        public async Task AddItem(TodoItem item)
        {
            if(!await CanAddItem(item))
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            db.Items.Add(item);
            await db.SaveChangesAsync();
        }

        async Task<bool> CanAddItem(TodoItem item)
        {
            SystemUser loggedUser = await loggedUserProvider.GetLoggedUser();
            return item.OwnerId == loggedUser.Id;
        }

        public async Task DeleteItem(int id)
        {
            if(!await CanGetOrDeleteItem(id))
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            TodoItem item = new TodoItem { Id = id };
            db.Items.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}
