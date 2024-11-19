using System.Security.Claims;

namespace BAExamApp.MVC.Areas.Admin.ViewComponents;

public class AuthenticatedUserViewComponent : ViewComponent
{
    private readonly IAdminService _adminService;
    public AuthenticatedUserViewComponent(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var admin = await _adminService.GetByIdentityIdAsync(userId);
        return View("Default", admin);
    }
}