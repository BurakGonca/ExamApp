namespace BAExamApp.Dtos.TestExams;
public class TestExamCreateDto
{
    public List<Guid>? SelectedQuestionIds { get; set; }
    public List<Guid>? SelectedTrainerIds { get; set; }
    public string Name { get; set; }
    public DateTime TestExamDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan TestExamDuration { get; set; }
    public int State { get; set; }
    public string? Description { get; set; }
}
