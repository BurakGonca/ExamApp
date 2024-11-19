using AspNetCoreHero.ToastNotification.Extensions;
using BAExamApp.BackgroundJobs;
using BAExamApp.Business.Extensions;
using BAExamApp.DataAccess.Contexts;
using BAExamApp.DataAccess.EFCore.Extensions;
using BAExamApp.DataAccess.Extensions;
using BAExamApp.Dtos.Emails;
using BAExamApp.MVC.Extensions;
using Hangfire;
using Microsoft.AspNetCore.Localization;
using OfficeOpenXml;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDataAccessServices(builder.Configuration)
    .AddEFCoreServices(builder.Configuration)
    .AddBusinessServices()
    .AddMvcServices()
    .AddHangfire(builder.Configuration)
    .Configure<EmailConfigurationDto>(builder.Configuration.GetSection("EmailConfiguration"))
    .AddHangfireServer() ;
    

GlobalConfiguration.Configuration
              .UseSqlServerStorage(builder.Configuration.GetConnectionString(BAExamAppDbContext.ConnectionName));

RecurringJob.AddOrUpdate<IStudentExamService>(
    service => service.CheckAndSendExamResultsAsync(),
    Cron.MinuteInterval(10),
    TimeZoneInfo.Local
);
RecurringJob.AddOrUpdate<IClassroomService>(
    "update-student-statuses",
    service => service.UpdateStudentStatusIfClassroomClosedAsync(),
    Cron.Daily(3, 1));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error/");
    app.UseHsts();
}

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
app.UseStatusCodePagesWithReExecute("/ErrorPage/ErrorIndex", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "Examm App HangFire Dashboard",
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

var cultures = new List<CultureInfo>
{
    new CultureInfo("tr")
};

app.UseRequestLocalization(options =>
{
    var supportedCultures = new[] { "en", "tr" };
    options.DefaultRequestCulture = new RequestCulture("tr");
    options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();

    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});

app.UseNotyf();
app.UseSession();
app.MapControllerRoute(name: "default",
                       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();

app.Run();