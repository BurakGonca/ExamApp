using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.QuestionSubtopics;
public class QuestionSubtopicsDto
{
    public Guid Id { get; set; }
    public Guid SubtopicId { get; set; }
    public Guid QuestionId { get; set; }
}
