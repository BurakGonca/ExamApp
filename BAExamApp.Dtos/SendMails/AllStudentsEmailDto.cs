using BAExamApp.Dtos.StudentExams;
using BAExamApp.Dtos.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.SendMails;
public class AllStudentsEmailDto
{
    public List<AllStudentExamResultDto> Students { get; set; }

}
