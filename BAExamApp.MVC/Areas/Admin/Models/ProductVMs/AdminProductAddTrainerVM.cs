using Microsoft.AspNetCore.Mvc.Rendering;

namespace BAExamApp.MVC.Areas.Admin.Models.ProductVMs;

public class AdminProductAddTrainerVM
{
    public Guid ProductId { get; set; }
    public List<Guid> SelectedTrainersIds { get; set; } = new();
    public List<SelectListItem> Trainers { get; set; } = new();
    public List<string>? AppointedTrainersId { get; set; }
}
