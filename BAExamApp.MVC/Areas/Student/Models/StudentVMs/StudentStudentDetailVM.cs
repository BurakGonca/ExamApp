using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Student.Models.StudentVMs;

public class StudentStudentDetailVM
{
    [Display(Name = "First_Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last_Name")]
    public string LastName { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }

    //[Display(Name = "Date_Of_Birth")]
    //public DateTime DateOfBirth { get; set; }

    [Display(Name = "Gender")]
    public bool Gender { get; set; }

    [Display(Name = "Profile_Image")]
    public string Image { get; set; }

    [Display(Name = "Graduation_Date")]
    [DataType(DataType.Date, ErrorMessage = "Lütfen geçerli bir tarih giriniz.")]
    public DateTime? GraduatedDate { get; set; }
}