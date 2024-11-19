namespace BAExamApp.Dtos.QuestionDifficulty;

public class QuestionDifficultyCreateDto
{
    public string Name { get; set; }
    public TimeSpan TimeGiven { get; set; }
    public double ScoreCoefficient { get; set; } = 1;
}
