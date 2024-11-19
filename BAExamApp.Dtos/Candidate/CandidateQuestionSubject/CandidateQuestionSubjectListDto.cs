using BAExamApp.Core.Enums;

namespace BAExamApp.Dtos.Candidate.CandidateQuestionSubject;

public class CandidateQuestionSubjectListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
}