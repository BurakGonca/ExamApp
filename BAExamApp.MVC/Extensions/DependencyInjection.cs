using AspNetCoreHero.ToastNotification;
using BAExamApp.Business.Services;
using BAExamApp.DataAccess.Contexts;
using BAExamApp.MVC.Authorization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Reflection;

namespace BAExamApp.MVC.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMvcServices(this IServiceCollection services)
    {
        services
            .AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
            .AddRazorRuntimeCompilation()
            .AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix,
            opt => opt.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                var assemblyName = new AssemblyName(typeof(SharedModelResource).GetTypeInfo().Assembly.FullName!);
                return factory.Create(nameof(SharedModelResource), assemblyName.Name!);
            });

        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services
            .AddHttpContextAccessor()
            .AddIdentityServices()
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddNotyf(options =>
            {
                options.IsDismissable = true;
                options.Position = NotyfPosition.BottomRight;
                options.HasRippleEffect = true;
            });
        services.AddSession();     
        return services;
    }

    public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClassroomService, ClassroomService>();
        services.AddHangfire(config =>
        {
            var option = new SqlServerStorageOptions
            {
                PrepareSchemaIfNecessary = true,
                QueuePollInterval = TimeSpan.FromMinutes(5),
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true,
            };
            config.UseSqlServerStorage(configuration.GetConnectionString(BAExamAppDbContext.ConnectionName), option).WithJobExpirationTimeout(TimeSpan.FromHours(6));
        });
        services.AddHangfireServer();
        return services;
    }


    private static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            /* TODO: Login girişleri kolaylaştırmak için şifre gereksinimleri basitleştirildi. Gereksinimler değiştirilecek.
             options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredUniqueChars = 1;
             */
            options.Password.RequiredLength = 4;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredUniqueChars = 0;
        })
            .AddEntityFrameworkStores<BAExamAppDbContext>()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = new PathString("/Login/Index");
            options.LogoutPath = new PathString("/Login/SignOut");
            options.Cookie = new CookieBuilder
            {
                Name = "BAExamAppCookie",
                HttpOnly = false,
                SameSite = SameSiteMode.Lax,
                SecurePolicy = CookieSecurePolicy.Always
            };
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(45);
            options.AccessDeniedPath = new PathString("/Login/AccessDenied");
        });

        return services;
    }
}
