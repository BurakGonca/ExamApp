using BAExamApp.Business.Extensions;
using BAExamApp.Business.Interfaces.Services;
using BAExamApp.Business.Services;
using BAExamApp.DataAccess.Contexts;
using BAExamApp.DataAccess.EFCore.Extensions;
using BAExamApp.DataAccess.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()
                                                    .AllowAnyOrigin()));
builder.Services
    .AddDataAccessServices(builder.Configuration)
    .AddEFCoreServices(builder.Configuration)
    .AddBusinessServices();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BAExamAppDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors();

app.UseRouting();

app.MapControllers();

app.Run();