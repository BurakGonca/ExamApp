using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.QuestionSubtopics
{
    public class QuestionSubtopicsListDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid SubtopicId { get; set; }

    }
}
