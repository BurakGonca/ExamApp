using BAExamApp.Entities.DbSets;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamInitiationVMs;

public class CandidateExamAnswerQuestionVM
{
    public Guid CandidateQuestionId { get; set; }
    public Guid ExamId { get; set; }
    public Guid CandidateId { get; set; }
    public Guid? CandidateAnswerId { get; set; }
    public int QuestionInOrder { get; set; }
    public string? Answer { get; set; }
    public CandidateQuestionType CandidateQuestionType { get; set; }
}
