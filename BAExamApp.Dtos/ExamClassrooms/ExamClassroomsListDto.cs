using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ExamClassrooms
{
    public class ExamClassroomsListDto
    {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public string ExamName { get; set; }
        public Guid ClassroomId { get; set; }
        public string ClassroomName { get; set; }
        public DateTime ExamDateTime { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public int? MaxStudentCount { get; set; }
        public int? StudentCount { get; set; }
        public bool IsFinished { get; set; }
    }
}
