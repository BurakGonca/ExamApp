using BAExamApp.Core.Enums;
using BAExamApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.QuestionRevisions;
public class QuestionRevisionDto
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string? ModifiedBy { get; set; } = null!;
    public DateTime? ModifiedDate { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public string RequestDescription { get; set; }
    public string? RevisionConclusion { get; set; }
    public virtual Question? Question { get; set; }
    public Guid QuestionId { get; set; }
    public Guid RequesterAdminId { get; set; }
    public Guid RequestedTrainerId { get; set; }
}
