using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BAExamApp.MVC.Authorization;

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
{
    private readonly IAccountService _accountService;
    public ApplicationUserClaimsPrincipalFactory(UserManager<IdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor, IAccountService accountService) : base(userManager, optionsAccessor)
    {
        _accountService = accountService;
    }

    protected async override Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        var userRoles = await UserManager.GetRolesAsync(user);
        if (!userRoles.Any())
        {
            return await base.GenerateClaimsAsync(user);
        }

        var userId = await _accountService.GetUserIdAsync(user.Id, userRoles.FirstOrDefault()!);
        if (userId != Guid.Empty)
        {
            var claims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.Add(new(ClaimTypes.NameIdentifier, userId.ToString()!));
            identity.AddClaims(claims);
        }

        return identity;
    }
}