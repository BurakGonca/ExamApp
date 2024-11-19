using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.SendMails;
public class ExamFinishedNotifyToCandidateAdminDto
{
    public string EmailAddress { get; set; }
    public Guid ExamId { get; set; }
    public string ExamName { get; set; }
    public string StudentFullName { get; set; }
    public Guid StudentId { get; set; }
    public int? Score { get; set; }
    public int MaxScore { get; set; }
    public List<string> Result { get; set; }
}
