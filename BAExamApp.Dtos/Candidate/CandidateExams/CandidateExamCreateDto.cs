using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExams;
public class CandidateExamCreateDto
{
    public string Name { get; set; } = null!;
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public string? Description { get; set; }
    public bool IsStarted { get; set; } = false;
    public int MaxScore { get; set; } = 100;
    public string? TrainerExplanation { get; set; }

    public int TestQuestionCount { get; set; }
    public int ClassicQuestionCount { get; set; }
    public int AlgorithmQuestionCount { get; set; }
    public int TestQuestionsCoefficient { get; set; }
    public int ClassicQuestionsCoefficient { get; set; }
    public int AlgorithmQuestionsCoefficient { get; set; }
    public bool forGroup { get; set; } = true;

    public List<Guid> CandidateIds { get; set; }
    public List<Guid> GroupIds { get; set; }
}
