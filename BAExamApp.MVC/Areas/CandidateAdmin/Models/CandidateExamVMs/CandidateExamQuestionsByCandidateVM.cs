using BAExamApp.Dtos.Candidate.CandidateExams;
using BAExamApp.MVC.Areas.Admin.Models.StudentQuestion;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamQuestionsByCandidateVM
{
    public Guid CandidateId { get; set; }
    public Guid ExamId { get; set; }
    public string FullName { get; set; }
    public string ExamName { get; set; }

    public CandidateExamQuestionsByCandidateVM()
    {
        Questions = new HashSet<CandidateExamQuestionsByCandidateListVM>();
    }

    public ICollection<CandidateExamQuestionsByCandidateListVM> Questions { get; set; }
}




