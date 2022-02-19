using _6_DataModificationControl.Data;
using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Transactions;

namespace _6_DataModificationControl
{
    public class DBInitializer
    {
        UserManager<SystemUser> userManager;
        RoleManager<UserRole> roleManager;
        ApplicationDbContext db;
        public DBInitializer(UserManager<SystemUser> userManager, RoleManager<UserRole> roleManager,
            ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = db;
        }

        public async Task InitializeDb()
        {
            await db.Database.MigrateAsync();
            await CreateRoles();
            await CreateUsers();

            var admins = await GetAdmin();
            Debug.Assert(admins.Count() > 0);
            var itemsForAdmin = CreateItemsForAdmin(admins.ElementAt(0));
            await InsertItems(itemsForAdmin);
        }

        async Task CreateRoles()
        {
            await CreateRole(RoleType.Admin);
            await CreateRole(RoleType.User);
        }

        async Task CreateRole(RoleType roleType)
        {
            if (await roleManager.RoleExistsAsync(roleType.ToString()))
                return;

            UserRole role = new UserRole();
            role.Name = roleType.ToString();

            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded)
                throw new InvalidOperationException("Could not create user role!");
        }

        async Task CreateUsers()
        {
            SystemUser admin = new SystemUser();
            admin.Email = "admin@example.com";
            admin.UserName = "admin";
            await CreateUser(RoleType.Admin, admin, "admin123");

            SystemUser user = new SystemUser();
            user.Email = "user@example.com";
            user.UserName = "user";
            await CreateUser(RoleType.User, user, "user123");            
        }

        async Task<IList<SystemUser>> GetAdmin()
        {
            return await userManager.GetUsersInRoleAsync(RoleType.Admin.ToString());
        }

        async Task CreateUser(RoleType roleType, SystemUser su, string pass)
        {
            var usersInRole = await userManager.GetUsersInRoleAsync(roleType.ToString());
            if (usersInRole.Count() > 0)
                return;

            using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await userManager.CreateAsync(su);
                if (!result.Succeeded)
                    throw new InvalidOperationException();

                result = await userManager.AddPasswordAsync(su, pass);
                if (!result.Succeeded)
                    throw new InvalidOperationException();

                result = await userManager.AddToRoleAsync(su, roleType.ToString());
                if (result.Succeeded)
                    tr.Complete();
                else
                    throw new InvalidOperationException();
            }
        }

        List<TodoItem> CreateItemsForAdmin(SystemUser su)
        {
            return new List<TodoItem>
            {
                new TodoItem {Title = "Update serwera", Description = "Zrobić aktualizację oprogramowania", Owner = su},
                new TodoItem {Title = "Znaleźć moderatora", Description = "Znaleźć moderatora do serwisu", Owner = su}
            };
        }

        async Task InsertItems(List<TodoItem> items)
        {
            using(var tr = db.Database.BeginTransaction())
            {
                foreach (var item in items)
                    db.Items.Add(item);

                await db.SaveChangesAsync();
                await tr.CommitAsync();
            }
        }
    }
}
