using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace IMDB_BE.Shared;

public class ClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        return GetClaim(ClaimTypes.NameIdentifier);
    }

    public string GetClaim(string key)
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value;
    }
}