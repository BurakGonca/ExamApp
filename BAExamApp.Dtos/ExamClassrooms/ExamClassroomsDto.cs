using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ExamClassrooms;
public class ExamClassroomsDto
{   
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid ClassroomId { get; set; }
    public string? ClassroomName { get; set; }
    public string? ExamName { get; set; }
    public string? ExamDescription { get; set; }
    public DateTime? ExamStartDate { get; set; }
    public DateTime? ExamEndDate { get; set; }    
}
