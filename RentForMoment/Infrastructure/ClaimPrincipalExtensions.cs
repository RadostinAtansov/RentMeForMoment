
namespace RentForMoment.Infrastructure
{
    using System.Security.Claims;
    using static WebConstants;

    public static class ClaimPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;


        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }

}
