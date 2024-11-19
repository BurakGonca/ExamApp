using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.StudentExams;
public class StudentExamResultDto
{
    public Dictionary<string, double> Score { get; set; }
    public Dictionary<string, int> RightAnswer { get; set; }
    public Dictionary<string, int> WrongAnswer { get; set; }
    public Dictionary<string, int> EmptyAnswer { get; set; }
}