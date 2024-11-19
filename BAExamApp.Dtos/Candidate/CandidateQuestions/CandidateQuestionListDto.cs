using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;

namespace BAExamApp.Dtos.Candidate.CandidateQuestions;
public class CandidateQuestionListDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int CandidateQuestionType { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    public Status Status { get; set; }
    public CandidateQuestionSubjectDto CandidateQuestionSubject { get; set; }
}
