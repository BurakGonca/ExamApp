using BAExamApp.Core.Entities.Base;
using BAExamApp.Core.Enums;
using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BAExamApp.Business.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAdminRepository _adminRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly ICandidateAdminRepository _candidateAdminRepository;

    public AccountService(UserManager<IdentityUser> userManager, IAdminRepository adminRepository, IStudentRepository studentRepository, ITrainerRepository trainerRepository, ICandidateAdminRepository candidateAdminRepository)
    {
        _userManager = userManager;
        _adminRepository = adminRepository;
        _studentRepository = studentRepository;
        _trainerRepository = trainerRepository;
        _candidateAdminRepository = candidateAdminRepository;
    }

    public async Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression)
    {
        return await _userManager.Users.AnyAsync(expression);
    }

    public async Task<IdentityResult> CreateUserAsync(IdentityUser user, Roles role)
    {
        IdentityResult result;
        //StringGenerator.GenerateRandomPassword(); //TODO:Mail entegrasyonundan sonra kullanıcıya bu şifre gönderilecek.
        result = await _userManager.CreateAsync(user, "newPassword+0"); //TODO:Password oluşturulup kullanıcıya mail olarak atılmalı
        if (!result.Succeeded)
        {
            return result;
        }

        return await _userManager.AddToRoleAsync(user, role.ToString());
    }

    public Task<IdentityUser?> FindByIdAsync(string identityId)
    {
        return _userManager.FindByIdAsync(identityId);
    }

    public async Task<IdentityResult> DeleteUserAsync(string identityId)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Code = nameof(Messages.UserNotFound),
                Description = Messages.UserNotFound
            });
        }

        return await _userManager.DeleteAsync(user);
    }

    public async Task<Guid> GetUserIdAsync(string identityId, string role)
    {
        BaseUser? user = role switch
        {
            "Admin" => await _adminRepository.GetByIdentityIdAsync(identityId),
            "Student" => await _studentRepository.GetByIdentityIdAsync(identityId),
            "Trainer" => await _trainerRepository.GetByIdentityIdAsync(identityId),
            "CandidateAdmin" => await _candidateAdminRepository.GetByIdentityIdAsync(identityId),
            _ => null
        };

        return user is null ? Guid.Empty : user.Id;
    }
    public async Task<IdentityUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> UpdateUserAsync(IdentityUser user)
    {
        return await _userManager.UpdateAsync(user);
    }
}