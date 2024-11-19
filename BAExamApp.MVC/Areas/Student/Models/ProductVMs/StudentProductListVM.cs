using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Student.Models.ProductVMs;

public class StudentProductListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Classroom_Name")]
    public string ClassroomName { get; set; }

    [Display(Name = "Product")]
    public string ProductName { get; set; }
}