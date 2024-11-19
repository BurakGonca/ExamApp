using BAExamApp.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BAExamApp.DataAccess.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BAExamAppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString(BAExamAppDbContext.ConnectionName),
                options => options.EnableRetryOnFailure(
                    10,
                    TimeSpan.FromSeconds(10),
                    null));
            options.UseLazyLoadingProxies();
        });

        return services;
    }
}