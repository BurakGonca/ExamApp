using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ExamClassrooms;
public class ExamClassroomForClassroomDetailsDto
{
    public Guid ExamId { get; set; }
    public string ExamName { get; set; }
    public string ExamRule { get; set; }
    public ExamType ExamType { get; set; }
    public DateTime ExamDateTime { get; set; }
    public TimeSpan ExamDuration { get; set; }
}
