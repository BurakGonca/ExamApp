namespace BAExamApp.MVC.Areas.Trainer.Models.ClassroomVMs;

public class TrainerClassroomExamAnalysisVM
{
    public TrainerClassroomExamAnalysisVM()
    {
        SubtopicPerformances = new Dictionary<string, double>();
    }
    public IDictionary<string, double> SubtopicPerformances
    { get; set; }
}
