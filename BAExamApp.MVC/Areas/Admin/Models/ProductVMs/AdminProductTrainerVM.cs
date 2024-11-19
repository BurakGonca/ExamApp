using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ProductVMs;

public class AdminProductTrainerVM
{
    public Guid TrainerId { get; set; }
    public Guid ProductId { get; set; }

    [Display(Name = "Full_Name")]
    public string? FullName { get; set; }

    [Display(Name = "Product_Created_Date")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Updated_Date")]
    public DateTime UpdatedDate { get; set; }
}
