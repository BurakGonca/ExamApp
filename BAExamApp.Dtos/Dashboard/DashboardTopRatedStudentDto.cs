using BAExamApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Dashboard;
public class DashboardTopRatedStudentDto
{
    public Guid Id { get; set; }
    public string StudentFullName { get; set; }
    public string LatestClassroom { get; set; }
    public decimal Score { get; set; }
    public int? StudentExamsCount { get; set; }
    public Status Status { get; set; }
   
}
