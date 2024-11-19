using BAExamApp.Dtos.ExamClassrooms;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Exams;
public class ExamUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public string? Description { get; set; }
    public ExamType ExamType { get; set; }
    public ExamCreationType ExamCreationType { get; set; }
    public decimal MaxScore { get; set; }
    public decimal BonusScore { get; set; }
    public Guid ExamRuleId { get; set; }
    public bool? IsTest { get; set; }

    public string? TrainerExplanation { get; set; }
    public List<StudentExamCreateDto> StudentExams { get; set; }
    public List<ExamClassroomsCreateDto> ExamClassroomsIds { get; set; }
}
