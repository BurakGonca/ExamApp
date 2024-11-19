using BAExamApp.MVC.Models;

namespace BAExamApp.MVC.Views.Shared.Components.Notifications;

public class NotificationsViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await Task.FromResult(new NotificationVM()));
    }
}
