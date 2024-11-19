using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.StudentExams;
public class StudentExamsAdminDto
{
    public Guid Id { get; set; }
    public decimal? Score { get; set; }
    public string ExamName { get; set; }
    public string MaxScore { get; set; }
    public DateTime ExamDateTime { get; set; }  
    public List<string> ClassroomNames { get; set; }
    public string StudentFullName { get; set; }
    public bool IsFinished { get; set; }
  
}
