using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Models;

public class LoginVM
{
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Doğrulama Kodu")]
    public int VerificationCode { get; set; } 
}