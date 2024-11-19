using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;

public class AdminClassroomCreateVM
{
    [Display(Name = "Classroom_Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Sınıf adı en az 2 karakterden oluşmalıdır")]
    public string Name { get; set; }

    [Display(Name = "Opening_Date")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [DataType(DataType.Date)]
    public DateTime OpeningDate { get; set; } = DateTime.Now;

    [Display(Name = "Closed_Date")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [DataType(DataType.Date)]
    public DateTime ClosedDate { get; set; } = DateTime.Now;

    [Display(Name = "GroupType")]
    [Required(ErrorMessage = "Lütfen eğitim tipi seçin.")]
    public Guid GroupTypeId { get; set; }

    [Display(Name = "Branch")]
    [Required(ErrorMessage = "Lütfen şube seçin.")]
    public Guid BranchId { get; set; }

    [Display(Name = "Product")]
    [Required(ErrorMessage = "Lütfen eğitim seçin.")]
    public List<Guid> ProductIds { get; set; }
}