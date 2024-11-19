using BAExamApp.Core.Enums;
using System.ComponentModel;

namespace BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;

public class AdminSubjectListVM
{
    public Guid Id { get; set; }

    [DisplayName("Name")]
    public string Name { get; set; }

    [DisplayName("Status")]
    public Status Status { get; set; }
}
