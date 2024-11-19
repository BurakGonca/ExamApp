using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services;
public class RoleService:IRoleService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    /// <summary>
    /// Seçilen Kullanıcının rollerini belirlenen özellikler ile birlikte döndürür.
    /// </summary>
    /// <param name="id">Seçilen kullanıcının rollerinşin bulunması için id'sine ihtiyaç vardır.</param>
    /// <returns>UserRoleAssingDto tipinde liste döner</returns>
    public async Task<IDataResult<List<UserRoleAssingDto>>> GetUserRoles(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        var roles = await _roleManager.Roles.ToListAsync();
        var userRoles = await _userManager.GetRolesAsync(user);
        List<UserRoleAssingDto> model = new List<UserRoleAssingDto>();
        foreach (var item in roles)
        {
            UserRoleAssingDto m = new UserRoleAssingDto();
            m.ID = item.Id;
            m.Name = item.Name;
            m.IsExist = userRoles.Contains(item.Name);
            model.Add(m);
        }
        return new SuccessDataResult<List<UserRoleAssingDto>>(model, Messages.ListedSuccess); ;
    }
    /// <summary>
    /// Seçilen kullanıcının rollerini düzenler
    /// </summary>
    /// <param name="model">Kullanıcının sahip olmasını istediğimiz rollerin listesi UserRoleAssingDto tipinde gereklidir</param>
    /// <param name="id">Seçilen user'ın bulunması için id'si gereklidir.</param>
    /// <returns>Sonucun başarılı olup olmaması durumunu döner.</returns>
    public async Task<IResult> UpdateUserRole(List<UserRoleAssingDto> userRoles, string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        foreach (var role in userRoles)
        {
            if (role.IsExist)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }
        }
        return new SuccessResult(Messages.UpdateSuccess);
    }
    /// <summary>
    /// Sistemdeki  log in olmuş kullanıcının rollerini string liste olarak döndürüyor.
    /// </summary>
    /// <param name="id">login olmuş kullanıcının rollerini bulmak için id'si gereklidir</param>
    /// <returns>Kullanıcının ait olduğu rollerin isimlerini string liste olarak döner</returns>
    public async Task<IList<string>> GetCurrentUserRoles(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        var userRoles = await _userManager.GetRolesAsync(user);
        return userRoles;
    }
    /// <summary>
    /// Sistemdki aktif kullanicilarin rollerini tek sayfa uzerinde degistirmek icin kullanilir
    /// </summary>
    /// <param name="userRoleUpdateDto"></param>
    /// <returns>IResult tipinde sonuc doner</returns>
    public async Task<IResult> ChangeUserRole(UserRoleUpdateDto userRoleUpdateDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userRoleUpdateDto.Id);
        if (await _userManager.IsInRoleAsync(user, userRoleUpdateDto.Role))
            await _userManager.RemoveFromRoleAsync(user, userRoleUpdateDto.Role);
        else
            await _userManager.AddToRoleAsync(user, userRoleUpdateDto.Role);

        return new SuccessResult(Messages.UpdateSuccess);
    }
    /// <summary>
    /// Tüm rolleri döner.
    /// </summary>
    /// <returns>Tüm rollerin listesini döner</returns>
    public async Task<IDataResult<List<string>>> GetAllRoles()
    {
        var roles = Enum.GetNames(typeof(Roles)).ToList();
        return new SuccessDataResult<List<string>>(roles, Messages.ListedSuccess);
    }
}
