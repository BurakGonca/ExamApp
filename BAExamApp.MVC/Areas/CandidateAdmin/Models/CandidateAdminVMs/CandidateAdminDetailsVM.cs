using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateAdminVMs;

public class CandidateAdminDetailsVM
{
    public Guid Id { get; set; }

    [Display(Name = "Full_Name")]
    public string FullName { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }    

    [Display(Name = "Profile_Image")]
    public string Image { get; set; }   

    [Display(Name = "OtherEmails")]
    public List<string>? OtherEmails { get; set; }
}
