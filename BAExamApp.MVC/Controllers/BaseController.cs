using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BAExamApp.MVC.Controllers;

public class BaseController : Controller
{
    protected string? UserIdentityId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    protected string? UserId => User.FindAll(ClaimTypes.NameIdentifier).LastOrDefault()?.Value;
    protected INotyfService NotyfService => HttpContext.RequestServices.GetService(typeof(INotyfService)) as INotyfService;
    protected IStringLocalizer<SharedModelResource> Localizer => HttpContext.RequestServices.GetService(typeof(IStringLocalizer<SharedModelResource>)) as IStringLocalizer<SharedModelResource>;

    protected void NotifySuccess(string message)
    {
        NotyfService.Success(message);
    }

    protected void NotifySuccessLocalized(string key)
    {
        NotyfService.Success(Localize(key));
    }

    protected void NotifyError(string message)
    {
        NotyfService.Error(message);
    }

    protected void NotifyErrorLocalized(string key)
    {
        NotyfService.Error(Localize(key));
    }

    protected void NotifyWarning(string message)
    {
        NotyfService.Warning(message);
    }

    protected void NotifyWarningLocalized(string key)
    {
        NotyfService.Warning(Localize(key));
    }

    protected string Localize(string key)
    {
        return Localizer.GetString(key);
    }
}