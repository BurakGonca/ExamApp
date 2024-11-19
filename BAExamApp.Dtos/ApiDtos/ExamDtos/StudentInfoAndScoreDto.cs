using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ApiDtos.ExamDtos;
public class StudentInfoAndScoreDto
{
    public string? StudentFirstName { get; set; }
    public string? StudentLastName { get; set; }
    public string? StudentEmail { get; set; }
    public decimal? ExamScore { get; set; }
}
