using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamQuestionItemsVM
{
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public CandidateQuestionSubjectDto CandidateQuestionSubject { get; set; }
    public int QuestionCount { get; set; }
}
