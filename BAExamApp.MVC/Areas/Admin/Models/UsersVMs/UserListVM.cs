using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.MVC.Areas.Admin.Models.UsersVMs;
public class UserListVM
{
    [Display(Name = "Kullanıcı ID")]
    public string Id { get; set; }
    [Display(Name ="Ad-Soyad")]
    public string FullName { get; set; }
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Display(Name = "Roller")]
    public List<UserRoleAssingDto>? UserRoles { get; set; }
}
