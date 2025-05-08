using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AuthService.Shared.Services;

public class ClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetUserId()
    {
        return Guid.TryParse(GetClaim(ClaimTypes.NameIdentifier), out var userId) ? userId : null;
    }

    public string GetClaim(string key)
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value;
    }
}
