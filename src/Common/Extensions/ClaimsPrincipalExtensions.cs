using System.Security.Claims;

namespace Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetId(this ClaimsPrincipal cp)
        {
            Claim idClaim = cp.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
                return null;

            int result = 0;
            if (!int.TryParse(idClaim.Value, out result))
                return null;

            return result;
        }
    }
}
