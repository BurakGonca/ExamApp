using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.AdminVMs;

public class AdminAdminListVM
{
    public Guid Id { get; set; }
    private string _firstName;

    [Display(Name = "First_Name")]
    public string FirstName{ get; set; }

    [Display(Name = "Last_Name")]
    public string LastName { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Profile_Image")]
    public byte[]? NewImage { get; set; }
}