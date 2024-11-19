using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Dashboard;
public class DasboardOverviewDTO
{
    public int StudentCount { get; set; }
    public int TrainerCount { get; set; }
    public int ExamCount { get; set; }
    public decimal SuccessRate { get; set; }
}
