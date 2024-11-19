using System.Security.Claims;

namespace BAExamApp.MVC.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static async Task<string> GetUserName(this ClaimsPrincipal user, IServiceProvider serviceProvider)
    {
        var adminService = serviceProvider.GetService<IAdminService>();

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var adminDto = await adminService.GetByIdentityIdAsync(userId);

        var adminFullName = adminDto.Data?.FirstName + " " + adminDto.Data?.LastName;

        return adminFullName;
    }

    public static async Task<string> GetTrainerUserName(this ClaimsPrincipal user, IServiceProvider serviceProvider)
    {
        var trainerService = serviceProvider.GetService<ITrainerService>();

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var trainerDto = await trainerService.GetByIdentityIdAsync(userId);

        var trainerFullName = trainerDto.Data?.FirstName + " " + trainerDto.Data?.LastName;

        return trainerFullName;
    }

    public static async Task<string> GetTrainerImage(this ClaimsPrincipal user, IServiceProvider serviceProvider)
    {
        var trainerService = serviceProvider.GetService<ITrainerService>();

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var trainerDto = await trainerService.GetByIdentityIdAsync(userId);

        return trainerDto.Data.Image;
    }
}
