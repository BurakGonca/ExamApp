using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.DbSets;
public class QuestionSubtopics : AuditableEntity
{
    public Guid QuestionId { get; set; }
    public virtual Question? Question { get; set; }

    public Guid SubtopicId { get; set; }
    public virtual Subtopic? Subtopic { get; set; }
}
