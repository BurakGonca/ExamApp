using BAExamApp.Dtos.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services;
public interface IRoleService
{
    /// <summary>
    /// Seçilen Kullanıcının rollerini belirlenen özellikler ile birlikte döndürür.
    /// </summary>
    /// <param name="id">Seçilen kullanıcının rollerinşin bulunması için id'sine ihtiyaç vardırç</param>
    /// <returns>UserRoleAssingDto tipinde liste döner</returns>
    Task<IDataResult<List<UserRoleAssingDto>>> GetUserRoles(string userId);
    /// <summary>
    /// Seçilen kullanıcının rollerini düzenler
    /// </summary>
    /// <param name="model">Kullanıcının sahip olmasını istediğimiz rollerin listesi UserRoleAssingDto tipinde gereklidir</param>
    /// <param name="id">Seçilen user'ın bulunması için id'si gereklidir.</param>
    /// <returns>Sonucun başarılı olup olmaması durumunu döner.</returns>
    Task<IResult> UpdateUserRole(List<UserRoleAssingDto> userRoleList, string userId);
    /// <summary>
    /// Sistemdeki  log in olmuş kullanıcının rollerini string liste olarak döndürüyor.
    /// </summary>
    /// <param name="id">login olmuş kullanıcının rollerini bulmak için id'si gereklidir</param>
    /// <returns>Kullanıcının ait olduğu rollerin isimlerini string liste olarak döner</returns>
    Task<IList<string>> GetCurrentUserRoles(string userId);
    /// <summary>
    /// Sistemdki aktif kullanicilarin rollerini tek sayfa uzerinde degistirmek icin kullanilir
    /// </summary>
    /// <param name="userRoleUpdateDto"></param>
    /// <returns>IResult tipinde sonuc doner</returns>
    Task<IResult> ChangeUserRole(UserRoleUpdateDto userRoleUpdateDto);
    /// <summary>
    /// Tüm rolleri döner.
    /// </summary>
    /// <returns>Tüm rollerin listesini döner</returns>
    Task<IDataResult<List<string>>> GetAllRoles();
}
