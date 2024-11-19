using BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;

namespace BAExamApp.Dtos.Candidate.CandidateQuestions;
public class CandidateQuestionUpdateDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int CandidateQuestionType { get; set; }
    public bool RemoveImage { get; set; }
    public byte[]? Image { get; set; }
    public List<CandidateAnswerUpdateDto> QuestionAnswers { get; set; }
    public Guid CandidateQuestionSubjectId { get; set; }
}
