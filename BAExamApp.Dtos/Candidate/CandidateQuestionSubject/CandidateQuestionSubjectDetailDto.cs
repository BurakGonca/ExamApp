using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Products;
using BAExamApp.Entities.DbSets;

namespace BAExamApp.Dtos.Candidate.CandidateQuestionSubject;
public class CandidateQuestionSubjectDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
}
