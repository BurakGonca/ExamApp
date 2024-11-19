using BAExamApp.Core.Enums;
using System.ComponentModel;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionSubjectVMs;

public class CandidateAdminQuestionSubjectListVM
{
    public Guid Id { get; set; }

    [DisplayName("Name")]
    public string Name { get; set; }

    [DisplayName("Status")]
    public Status Status { get; set; }
}
