using BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Dtos.Candidate.CandidateQuestions;
public class CandidateQuestionCreateDto
{
    public string? Content { get; set; }
    public byte[]? Image { get; set; }
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public List<CandidateAnswerCreateDto> QuestionAnswers { get; set; }
    public Guid? CandidateQuestionSubjectId { get; set; }
}
