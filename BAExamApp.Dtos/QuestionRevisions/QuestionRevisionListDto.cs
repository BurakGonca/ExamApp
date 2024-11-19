using BAExamApp.Core.Enums;
using BAExamApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.QuestionRevisions;
public class QuestionRevisionListDto
{
    //Automapped properties
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string RequestDescription { get; set; }
    public string? RevisionConclusion { get; set; }
    public string RequesterAdminName { get; set; }
    public string RequestedTrainerName { get; set; }
}
