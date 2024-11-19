using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Student.Models.StudentVMs;

public class StudentStudentUpdateVM
{
    public Guid Id { get; set; }
    [Display(Name = "First_Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last_Name")]
    public string LastName { get; set; }

    [Display(Name = "Adress")]
    public string Address { get; set; }

    public string? Image { get; set; }

    [Display(Name = "Profile_Image")]
    public IFormFile? NewImage { get; set; }

    //[Display(Name = "Date_Of_Birth")]
    //[DataType(DataType.Date, ErrorMessage = "Lütfen geçerli bir tarih giriniz.")]
    //public DateTime DateOfBirth { get; set; }

    [Display(Name = "Gender")]
    public bool Gender { get; set; }


    [Display(Name = "Graduation_Date")]
    [DataType(DataType.Date, ErrorMessage = "Lütfen geçerli bir tarih giriniz.")]
    public DateTime? GraduatedDate { get; set; }

    public string Email { get; set; }
    public List<string>? OtherEmails { get; set; }

}