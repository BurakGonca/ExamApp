using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;

public class CandidateExamCreateVM
{
    public string Name { get; set; } = null!;

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime ExamDateTime { get; set; }

    public TimeSpan ExamDuration { get; set; }
    public string? Description { get; set; }
    public bool IsStarted { get; set; } = false;
    public int MaxScore { get; set; } = 100;
    public string? TrainerExplanation { get; set; }

    public int TestQuestionCount { get; set; } = 0;
    public int ClassicQuestionCount { get; set; } = 0;
    public int AlgorithmQuestionCount { get; set; } = 0;
    public int TestQuestionsCoefficient { get; set; }
    public int ClassicQuestionsCoefficient { get; set; }
    public int AlgorithmQuestionsCoefficient { get; set; }
    public bool forGroup { get; set; } = true;
    public DateTime? ExamLinkEndDate { get; set; }
    public int? LinkValidityPeriod { get; set; }

    public List<Guid> CandidateIds { get; set; }
    public List<Guid> GroupIds { get; set; }

}
