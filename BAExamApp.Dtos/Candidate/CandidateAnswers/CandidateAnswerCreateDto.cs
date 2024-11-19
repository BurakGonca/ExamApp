namespace BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;
public class CandidateAnswerCreateDto
{
    public string Answer { get; set; } 
    public bool IsImageAnswer { get; set; }
    public bool IsRightAnswer { get; set; }
    public Guid QuestionId { get; set; }
}
