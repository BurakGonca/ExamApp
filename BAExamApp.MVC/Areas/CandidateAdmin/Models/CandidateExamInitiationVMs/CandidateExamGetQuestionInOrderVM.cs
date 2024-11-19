using BAExamApp.Dtos.Candidate.CandidateExams;
using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateAnswerVMs;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamInitiationVMs;

public class CandidateExamGetQuestionInOrderVM
{
    public Guid CandidateQuestionId { get; set; }
    public Guid ExamId { get; set; }
    public Guid CandidateId { get; set; }
    public int QuestionInOrder { get; set; }
    public string Name { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public DateTime ExamDateTime { get; set; }
    public int QuestionCount { get; set; }
    public string Content { get; set; }
    public byte[]? Image { get; set; }
    public string Answer { get; set; }
    public Guid CandidateAnswerId { get; set; }
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public List<CandidateAnswerListVM> QuestionAnswers { get; set; }
}
