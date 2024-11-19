using BAExamApp.Dtos.Candidate.CandidateExams;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamDetailsVM
{
    public Guid Id { get; set; }
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
    public List<CandidateExamDetailsListVM> CandidateList { get; set; }

}
