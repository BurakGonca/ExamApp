using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Trainers;
using BAExamApp.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services;
public interface IUserService
{
    /// <summary>
    /// Sistemde Aktif olan tüm kullanıcıları ve rollerini listeler.
    /// </summary>
    /// <returns>UserListDto tipinde liste döner</returns>
    Task<IDataResult<List<UserListDto>>> GetAllAsync();

    Task<string> GetEmailByUserId(string userId, Roles role);
}
