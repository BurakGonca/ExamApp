using BAExamApp.MVC.Areas.Trainer.Models.QuestionRevisionVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;

namespace BAExamApp.MVC.Areas.Trainer.Models.Revision;

public class RevisionDetails
{
    public IEnumerable<TrainerQuestionRevisionListVM> QuestionRevisionListVMs { get; set; }
    public TrainerQuestionDetailsVM QuestionDetails { get; set; }
}
