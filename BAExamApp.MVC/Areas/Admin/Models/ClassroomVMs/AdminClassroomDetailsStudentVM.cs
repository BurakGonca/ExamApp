using BAExamApp.Entities.DbSets;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;

public class AdminClassroomDetailsStudentVM
{
   
    public Guid Id { get; set; }

    [Display(Name = "First_Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last_Name")]
    public string LastName { get; set; }

    [Display(Name = "e-mail")]
    public string Email { get; set; }

    [Display(Name = "e-mail")]
    public string PhoneNumber { get; set; }





}
