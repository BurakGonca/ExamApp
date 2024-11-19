using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Admins;
using BAExamApp.Dtos.Users;
using BAExamApp.Entities.DbSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Business.Services;
public class UserService : IUserService
{

    private readonly IAdminRepository _adminRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;
    private readonly IApiUserRepository _apiUserRepository;
    private readonly ICandidateAdminRepository _candidateAdminRepository;

    public UserService(IMapper mapper, IAdminRepository adminRepository, ITrainerRepository trainerRepository, IStudentRepository studentRepository, IRoleService roleService, IApiUserRepository apiUserRepository, ICandidateAdminRepository candidateAdminRepository)
    {

        _mapper = mapper;
        _adminRepository = adminRepository;
        _trainerRepository = trainerRepository;
        _studentRepository = studentRepository;
        _roleService = roleService;
        _apiUserRepository = apiUserRepository;
        _candidateAdminRepository = candidateAdminRepository;
    }
    /// <summary>
    /// Sistemde Aktif olan tüm kullanıcıları ve rollerini listeler.
    /// </summary>
    /// <returns>UserListDto Tipinde Liste Döner</returns>
    public async Task<IDataResult<List<UserListDto>>> GetAllAsync()
    {
        var adminList = await _adminRepository.GetAllAsync();
        var trainerList = await _trainerRepository.GetAllAsync();
        var studentList = await _studentRepository.GetAllAsync();
        var apiUserList = await _apiUserRepository.GetAllAsync();

        List<UserListDto> users = new List<UserListDto>();
        users.AddRange(adminList.Select(x => new UserListDto() { ID = x.IdentityId, FullName = x.FullName, Email = x.Email, IdentityId = x.IdentityId }).ToList());
        users.AddRange(trainerList.Select(x => new UserListDto() { ID = x.IdentityId, FullName = x.FullName, Email = x.Email, IdentityId = x.IdentityId }).ToList());
        users.AddRange(studentList.Select(x => new UserListDto() { ID = x.IdentityId, FullName = x.FullName, Email = x.Email, IdentityId = x.IdentityId }).ToList());
        users.AddRange(apiUserList.Select(x => new UserListDto() { ID = x.IdentityId, FullName = x.FullName, Email = x.Email, IdentityId = x.IdentityId }).ToList());
        
        foreach (var user in users)
        {
            var addRoles = await _roleService.GetUserRoles(user.IdentityId);
            user.UserRoles = addRoles.Data;
        }
        return new SuccessDataResult<List<UserListDto>>(users, Messages.ListedSuccess); ;
    }

    public async Task<string> GetEmailByUserId(string userId, Roles role)
    {
        if (role == Roles.Admin)
        {
            var admin = await _adminRepository.GetByIdentityIdAsync(userId);
            return admin.Email;
        }
        else if (role == Roles.ApiUser)
        {
            var apiUser = await _apiUserRepository.GetByIdentityIdAsync(userId);
            return apiUser.Email;
        }
        else if (role == Roles.CandidateAdmin)
        {
            var candidateAdmin = await _candidateAdminRepository.GetByIdentityIdAsync(userId);
            return candidateAdmin.Email;
        }
        else if (role == Roles.Student)
        { 
            var student = await _studentRepository.GetByIdentityIdAsync(userId);
            return student.Email;
        }
        var trainer = await _trainerRepository.GetByIdentityIdAsync(userId);
        return trainer.Email;
    }
}