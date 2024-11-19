using BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;
using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExamInitiation;
public class CandidateExamGetQuestionInOrderDto
{
    public Guid CandidateQuestionId { get; set; }
    public Guid ExamId { get; set; }
    public Guid CandidateId { get; set; }
    public int QuestionInOrder { get; set; }
    public string Name { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public DateTime ExamDateTime { get; set; }
    public int QuestionCount { get; set; }
    public string Content { get; set; }
    public byte[]? Image { get; set; }
    public string Answer { get; set; }
    public Guid CandidateAnswerId { get; set; }
    public CandidateQuestionType CandidateQuestionType { get; set; }
    public List<CandidateAnswerDto> QuestionAnswers { get; set; }
}
