
namespace RentForMoment.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
