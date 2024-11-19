using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ApiDtos.ExamApiDtos;
public class ExamResultDto
{
    public string ExamName { get; set; }
    public decimal? Score { get; set; }
}
