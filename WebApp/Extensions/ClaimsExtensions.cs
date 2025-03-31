using System.Security.Claims;

namespace WebApp.Extensions;

public static class ClaimsExtensions
{
    public static string? GetUserName(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.GivenName);
            // user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            // user.Claims.SingleOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
    }
}