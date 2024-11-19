using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.TechnicalUnitVMs;

public class AdminTechnicalUnitListVM
{
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }
}