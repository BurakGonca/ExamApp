using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.TechnicalUnitVMs;

public class AdminTechnicalUnitDetailsVM
{

    public Guid Id { get; set; }

    [Display(Name = "TechnicalUnit_Name")]
    public string Name { get; set; }
}