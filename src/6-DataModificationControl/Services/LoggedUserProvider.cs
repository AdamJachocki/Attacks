using _6_DataModificationControl.Abstractions;
using Common.Models;
using Microsoft.AspNetCore.Identity;

namespace _6_DataModificationControl.Services
{
    public class LoggedUserProvider : ILoggedUserProvider
    {
        readonly IHttpContextAccessor ctx;
        readonly UserManager<SystemUser> userMan;

        public LoggedUserProvider(IHttpContextAccessor ctx, UserManager<SystemUser> userMan)
        {
            this.ctx = ctx;
            this.userMan = userMan;
        }

        public async Task<SystemUser> GetLoggedUser()
        {
            return await userMan.GetUserAsync(ctx.HttpContext?.User);
        }

        public async Task<bool> IsLoggedAdmin()
        {
            SystemUser user = await GetLoggedUser();
            if (user == null)
                return false;

            return await userMan.IsInRoleAsync(user, RoleType.Admin.ToString());

        }
    }
}
