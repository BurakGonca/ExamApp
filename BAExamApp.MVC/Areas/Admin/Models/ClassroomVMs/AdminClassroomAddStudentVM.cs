using Microsoft.AspNetCore.Mvc.Rendering;

namespace BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;

public class AdminClassroomAddStudentVM
{
    public Guid ClassroomId { get; set; }
    public List<SelectListItem> Students { get; set; } = new();
    public List<Guid> SelectedStudentIds { get; set; } = new();
    public List<string>? AppointedStudentsId { get; set; }
}