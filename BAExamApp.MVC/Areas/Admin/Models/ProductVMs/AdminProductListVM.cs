using BAExamApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.ProductVMs;

public class AdminProductListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    public bool IsActive { get; set; }
    [Display(Name = "Status")]
    public Status Status { get; set; }
}