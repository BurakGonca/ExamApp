using BAExamApp.Core.Enums;
using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BAExamApp.DataAccess.EFCore.Seeds;

internal static class AdminSeed
{
    private const string AdminEmail = "*******@baexamapp.com";
    private const string AdminPassword = "*******";
    //private const string DefaultCity = "İstanbul";

    private const string CandidateAdminEmail = "*******@baexamapp.com";
    private const string CandidateAdminPassword = "*******";
    private const string BranchName = "İstanbul";
    public static async Task SeedAsync(IConfiguration configuration)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<BAExamAppDbContext>();

        dbContextBuilder.UseSqlServer(configuration.GetConnectionString(BAExamAppDbContext.ConnectionName));

        using BAExamAppDbContext context = new(dbContextBuilder.Options);
        if (!context.Roles.Any())
        {
            await AddRoles(context);
        }

        if (!context.Users.Any(user => user.Email == AdminEmail))
        {
            await AddAdmin(context);
        }

        // Burda Candidate Admin Oluşturuluyor
        if(!context.Users.Any(user => user.Email == CandidateAdminEmail))
        {
            await AddCandidateAdmin(context);
        }

        await Task.CompletedTask;
    }

    private static async Task AddAdmin(BAExamAppDbContext context)
    {
        IdentityUser user = new()
        {
            UserName = AdminEmail,
            NormalizedUserName = AdminEmail.ToUpperInvariant(),
            Email = AdminEmail,
            NormalizedEmail = AdminEmail.ToUpperInvariant(),
            EmailConfirmed = true
        };
        user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, AdminPassword);
        await context.Users.AddAsync(user);

        var adminRoleId = context.Roles.FirstOrDefault(role => role.Name == Roles.Admin.ToString())!.Id;

        await context.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = user.Id, RoleId = adminRoleId });

     

        context.Admins.Add(new Admin
        {
            Status = Status.Added,
            CreatedBy = "Super-Admin",
            CreatedDate = DateTime.Now,
            ModifiedBy = "Super-Admin",
            ModifiedDate = DateTime.Now,
            FirstName = "Admin",
            LastName = "Admin",
            Email = AdminEmail,
            Gender = true,
            //DateOfBirth = new DateTime(2000, 1, 1),
            IdentityId = user.Id,
        });

        await context.SaveChangesAsync();
    }

    private static async Task AddCandidateAdmin(BAExamAppDbContext context)
    {
        IdentityUser user = new()
        {
            UserName = CandidateAdminEmail,
            NormalizedUserName = CandidateAdminEmail.ToUpperInvariant(),
            Email = CandidateAdminEmail,
            NormalizedEmail = CandidateAdminEmail.ToUpperInvariant(),
            EmailConfirmed = true
        };
        user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, CandidateAdminPassword);
        await context.Users.AddAsync(user);

        var candidateAdminRoleId = context.Roles.FirstOrDefault(role => role.Name == Roles.CandidateAdmin.ToString())!.Id;

        await context.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = user.Id, RoleId = candidateAdminRoleId });



        context.CandidateAdmins.Add(new CandidateCandidateAdmin
        {
            Status = Status.Added,
            CreatedBy = "Candidate-Admin",
            CreatedDate = DateTime.Now,
            ModifiedBy = "Candidate-Admin",
            ModifiedDate = DateTime.Now,
            FirstName = "CandidateAdmin",
            LastName = "CandidateAdmin",
            Email = CandidateAdminEmail,
            Gender = true,           
            IdentityId = user.Id,
        });

        await context.SaveChangesAsync();
    }

    private static async Task AddRoles(BAExamAppDbContext context)
    {
        string[] roles = Enum.GetNames(typeof(Roles));
        for (int i = 0; i < roles.Length; i++)
        {
            if (await context.Roles.AnyAsync(role => role.Name == roles[i]))
            {
                continue;
            }

            await context.Roles.AddAsync(new IdentityRole(roles[i]));
        }
    }
}

