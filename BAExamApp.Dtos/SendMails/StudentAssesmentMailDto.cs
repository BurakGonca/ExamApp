namespace BAExamApp.Dtos.SendMails;
public class StudentAssesmentMailDto
{
    public string Assessment { get; set; }
    public string StudentEmailAddress { get; set; }
    public string ExamName { get; set; }
    public string TrainerName { get; set; }
    public List<string> Result { get; set; }
}
