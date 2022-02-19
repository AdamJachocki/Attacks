using Microsoft.AspNetCore.Identity;

namespace Common.Models
{
    public enum RoleType
    {
        Admin,
        User
    }
    public class UserRole: IdentityRole<int>
    {
    }
}
