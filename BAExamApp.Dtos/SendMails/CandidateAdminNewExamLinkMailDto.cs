using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.SendMails;
public class CandidateAdminNewExamLinkMailDto
{
    public string CandidateAdminEmailAdress { get; set; }
    public string ExamLink { get; set; }
}
