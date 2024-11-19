using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.StudentExams;
public class StudentExamsDetailsDto
{

    public Guid Id { get; set; }
    public decimal? Score { get; set; }
    public string ExamName { get; set; }
   
    public string? ExcuseDescription { get; set; }
    public string MaxScore { get; set; }
    public DateTime ExamDateTime { get; set; }
    public List<string> ClassroomNames { get; set; }
    public string StudentFullName { get; set; }
    public string LatestClassroomsTrainers { get; set; }
    public string LatestClassroom { get; set; }
    public List<string> StudentClassroomNames { get; set; }

    public bool IsTest { get; set; }
    public bool IsCanceled { get; set; }
    public ExamType ExamType { get; set; }
}
