using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;

public class AdminSubjectCreateVM
{
    [DisplayName("Subject")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Konu adı en az 2 karakterden oluşmalıdır.")]
    public string Name { get; set; }

    [Display(Name = "Product_Name")]
    [Required(ErrorMessage = "Ürün Seçmeden Konu Eklenemez.")]
    public List<Guid> ProductIds { get; set; }

    public SelectList? ProductSelectList { get; set; }
    
}