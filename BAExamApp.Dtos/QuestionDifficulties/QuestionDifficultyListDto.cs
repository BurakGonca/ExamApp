namespace BAExamApp.Dtos.QuestionDifficulty;

public class QuestionDifficultyListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TimeSpan TimeGiven { get; set; }
    public double ScoreCoefficient { get; set; }
}
