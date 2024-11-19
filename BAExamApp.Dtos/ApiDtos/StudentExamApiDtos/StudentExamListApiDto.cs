using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ApiDtos.StudentExamApiDtos;
public class StudentExamListApiDto
{
    public Guid StudentId { get; set; }
    public string FullName { get; set; }
    public decimal? Score { get; set; }
    public string ExamName { get; set; }
}
