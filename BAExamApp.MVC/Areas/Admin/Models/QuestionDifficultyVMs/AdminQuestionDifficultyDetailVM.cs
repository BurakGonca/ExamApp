using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionDifficultyVMs;

public class AdminQuestionDifficultyDetailVM
{
    public Guid Id { get; set; }

    [Display(Name = "QuesitionDifficulty_Name")]
    public string Name { get; set; }

    [Display(Name = "QuesitionDifficulty_Time")]
    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:mm\:ss}")]
    public TimeSpan TimeGiven { get; set; }

    [Display(Name = "Score_Coefficient")]
    public double ScoreCoefficient { get; set; }
}
