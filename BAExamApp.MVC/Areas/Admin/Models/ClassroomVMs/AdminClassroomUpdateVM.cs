using BAExamApp.Dtos.StudentClassrooms;
using BAExamApp.Dtos.TrainerClassrooms;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;

public class AdminClassroomUpdateVM
{

    public Guid Id{ get; set; }

    [Display(Name = "Classroom_Name")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [MinLength(2, ErrorMessage = "Sınıf adı en az 2 karakterden oluşmalıdır")]
    public string Name { get; set; }

    [Display(Name = "Opening_Date")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [DataType(DataType.Date)]
    public DateTime OpeningDate { get; set; }

    [Display(Name = "Closed_Date")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
    [DataType(DataType.Date)]
    public DateTime ClosedDate { get; set; }
    
    [Display(Name = "GroupType_Id")]
    [Required(ErrorMessage = "Lütfen eğitim tipi seçin.")]
    public Guid GroupTypeId { get; set; }

    [Display(Name = "Branch_Id")]
    [Required(ErrorMessage = "Lütfen şube seçin.")]
    public Guid BranchId { get; set; }

    public SelectList? GroupTypeList { get; set; }
    public SelectList? BranchList { get; set; }

    [Display(Name = "Product_List")]
    public SelectList? ProductList { get; set; }

    [Display(Name = "Product_List")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
    public List<Guid>? ProductIds { get; set; }
    public List<StudentClassroomListForClassroomDetailsForAdminDto> StudentClassrooms { get; set; }
    public List<TrainerClassroomListForClassroomDetailsDto> TrainerClassrooms { get; set; }

    
}