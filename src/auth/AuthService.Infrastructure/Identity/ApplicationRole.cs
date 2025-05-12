using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Identity;

public class ApplicationRole(string name) : IdentityRole<Guid>(name) { }
