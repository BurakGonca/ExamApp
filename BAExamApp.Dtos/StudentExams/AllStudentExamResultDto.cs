using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.StudentExams;
public class AllStudentExamResultDto
{
    public Guid StudentId { get; set; }
    public string StudentFullName { get; set; }
    public string ExamName { get; set; }
    public decimal? Score { get; set; }

}
