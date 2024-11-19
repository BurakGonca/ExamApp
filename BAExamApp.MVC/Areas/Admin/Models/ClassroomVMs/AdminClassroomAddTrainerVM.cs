using Microsoft.AspNetCore.Mvc.Rendering;

namespace BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;

public class AdminClassroomAddTrainerVM
{
    public Guid ClassroomId { get; set; }
    public List<Guid> SelectedTrainersIds { get; set; } = new();
    public List<SelectListItem> Trainers { get; set; } = new();
    public List<string>? AppointedTrainersId { get; set; }
}