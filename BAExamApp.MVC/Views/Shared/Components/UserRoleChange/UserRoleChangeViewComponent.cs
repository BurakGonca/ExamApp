using System.Security.Claims;

namespace BAExamApp.MVC.Views.Shared.Components.UserRoleChange;

public class UserRoleChangeViewComponent:ViewComponent
{
    private readonly IRoleService roleService;

    public UserRoleChangeViewComponent(IRoleService roleService)
    {
        this.roleService = roleService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value ;
        var userRoleList = await roleService.GetCurrentUserRoles(userID);
        return View(userRoleList);
    }
}
