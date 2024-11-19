using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;

namespace BAExamApp.Dtos.Candidate.CandidateQuestions;
public class CandidateQuestionDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int CandidateQuestionType { get; set; }
    public byte[]? Image { get; set; }
    public Guid CandidateQuestionSubjectId { get; set; }
    public Status Status { get; set; }
    public List<CandidateAnswerDto> QuestionAnswers { get; set; }
}
