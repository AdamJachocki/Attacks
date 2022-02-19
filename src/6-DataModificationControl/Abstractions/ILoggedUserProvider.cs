using Common.Models;

namespace _6_DataModificationControl.Abstractions
{
    public interface ILoggedUserProvider
    {
        Task<SystemUser> GetLoggedUser();
        Task<bool> IsLoggedAdmin();
    }
}
