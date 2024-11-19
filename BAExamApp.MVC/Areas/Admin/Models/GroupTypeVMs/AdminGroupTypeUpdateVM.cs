using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.GroupTypeVMs;

public class AdminGroupTypeUpdateVM
{
    public Guid Id { get; set; }
    [Display(Name = "Group_Type")]
    [Required(ErrorMessage = "Eğitim tipi boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Eğitim Tipi en az 2 karakterden oluşmalıdır.")]
    [RegularExpression(@"^(?!'+$)[a-zA-Z'ğüşöçİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçİĞÜŞÖÇ]+)*$", ErrorMessage = "Eğitim Tipi özel karakter ya da sayı içeremez.")]
    public string Name { get; set; }
    [Display(Name = "Information")]
    [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
    [MaxLength(500, ErrorMessage = "Açıklama 500 karakterden uzun olamaz")]
    public string Information { get; set; }
}