using BAExamApp.Dtos.Classrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Profiles.MappingConfigurations;
public class MappingConfigurations
{
    public static void MapsterConfig()
    {
        TypeAdapterConfig<Classroom, ClassroomListDto>.NewConfig()
            .Map(dest => dest.BranchName, src => src.Branch.Name)
            .Map(dest => dest.StudentCount, src => src.StudentClassrooms.Count(c => c.Status != Core.Enums.Status.Deleted))
            .Map(dest => dest.ClassroomExamCount, src => src.ExamClassrooms.Count(x => x.Exam.IsTest == false && x.Exam.IsCanceled == false))
            .Map(dest => dest.ClassroomAppointedExamCount, src => src.ExamClassrooms.Count(x => x.Exam.IsStarted == true && x.Exam.IsTest == false && x.Exam.IsCanceled == false));
    }
}
