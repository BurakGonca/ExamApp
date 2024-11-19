using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ApiDtos.ExamDtos;
public class GetAllDataWithRegisterCodeDto
{
    public string? ExamClassroom { get; set; }
    public string? ExamName { get; set; }
    public string? ExamRule { get; set; }
    public List<StudentInfoAndScoreDto> StudentInfo { get; set; }
}
