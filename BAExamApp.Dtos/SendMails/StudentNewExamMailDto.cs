namespace BAExamApp.Dtos.SendMails;
public class StudentNewExamMailDto
{
    public string EmailAdress { get; set; }
    public string Url { get; set; }
    public Guid StudentExamId { get; set; }
    public DateTime ExamDate { get; set; }
    public TimeSpan ExamDuration { get; set; }
    public string ExamName { get; set; }
}
