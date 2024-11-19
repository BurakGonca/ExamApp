namespace BAExamApp.MVC.Views.Shared.Components.UserMenu;

public class UserMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View(Enumerable.Empty<string>()));
    }
}
