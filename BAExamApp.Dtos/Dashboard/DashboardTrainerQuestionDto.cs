using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Dashboard;
public class DashboardTrainerQuestionDto
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public State State { get; set; }

    public DateTime CreatedDate { get; set; }

}
