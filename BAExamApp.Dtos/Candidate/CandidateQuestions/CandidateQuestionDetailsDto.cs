using BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;
using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;

namespace BAExamApp.Dtos.Candidate.CandidateQuestions;
public class CandidateQuestionDetailsDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int CandidateQuestionType { get; set; }
    public byte[]? Image { get; set; }
    public List<CandidateAnswerDto> QuestionAnswers { get; set; }
    public DateTime CreatedDate { get; set; }
    public CandidateQuestionSubjectDto CandidateQuestionSubject { get; set; }

}
