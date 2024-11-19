using BAExamApp.Entities.DbSets;
using System.ComponentModel.DataAnnotations;


namespace BAExamApp.MVC.Areas.Admin.Models.StudentVMs;

public class AdminStudentListVM
{
    public Guid Id { get; set; }
    private string _firstName;

    [Display(Name = "FirstName")]
    public string FirstName
    {
        get => _firstName;
        set => _firstName = string.Join(" ", value.Split(' ')
                                                   .Select(n => char.ToUpper(n[0]) + n.Substring(1).ToLower()));
    }
    [Display(Name = "LastName")]
    public string LastName { get; set; }
    [Display(Name = "Email")]
    public string Email { get; set; }
    public byte[]? NewImage { get; set; }

    
}