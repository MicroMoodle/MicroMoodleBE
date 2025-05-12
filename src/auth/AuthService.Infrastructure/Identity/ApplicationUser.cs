using AuthService.Core.Common;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
}
