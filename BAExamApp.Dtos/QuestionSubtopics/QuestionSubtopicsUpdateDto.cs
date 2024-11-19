using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.QuestionSubtopics;
public class QuestionSubtopicsUpdateDto
{
    public Guid? Id { get; set; }
    public Guid? QuestionId { get; set; }
    public Guid? SubtopicId { get; set; }
}
