using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.SendMails;
public class StudentTrainerCancelExamMailDto
{
    public string EmailAdress { get; set; }
    public string ExamName { get; set; }
    public DateTime ExamDate { get; set; }
}
