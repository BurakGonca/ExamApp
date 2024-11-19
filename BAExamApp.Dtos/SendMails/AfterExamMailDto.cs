namespace BAExamApp.Dtos.SendMails;
public class AfterExamMailDto
{
    public string Email { get; set; }
    public string StudentFullName { get; set; }
    public string ExamName { get; set; }
    public int TotalTimeSpent { get; set; }
    public int? StudentPoint { get; set; }
}
