using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ExamClassrooms
{
    public class ExamClassroomsAddDto
    {
        public Guid ExamId { get; set; }
        public Guid ClassroomId { get; set; }
    }
}
