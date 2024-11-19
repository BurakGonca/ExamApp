using BAExamApp.Dtos.StudentClassrooms;

namespace BAExamApp.Business.Profiles;
public class StudentClassroomProfile : Profile
{
    public StudentClassroomProfile()
    {
        CreateMap<StudentClassroom, StudentClassroomDto>();
        CreateMap<StudentClassroom, StudentClassroomListForStudentDto>()
            .ForMember(dest => dest.ClassroomId, opt => opt.MapFrom(src => src.Classroom.Id))
            .ForMember(dest => dest.ClassroomName, opt => opt.MapFrom(src => src.Classroom.Name))
            .ForMember(dest => dest.OpeningDate, opt => opt.MapFrom(src => src.Classroom.OpeningDate))
            .ForMember(dest => dest.ClosedDate, opt => opt.MapFrom(src => src.Classroom.ClosedDate))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Classroom.Branch.Name));
        CreateMap<StudentClassroom, StudentClassroomListForClassroomDetailsDto>()
            .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.FullName));
        CreateMap<StudentClassroom, StudentClassroomListForClassroomDetailsForAdminDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Student.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Student.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Student.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Student.Email))
            .ForMember(dest => dest.StudentExamCount, opt => opt.MapFrom(src => src.Student.StudentExams.Count(x => x.Exam.IsTest == false && x.Exam.IsCanceled == false)))
            .ForMember(dest => dest.StudentAppointedExamCount, opt => opt.MapFrom(src => src.Student.StudentExams.Where(x=>x.Exam.IsStarted==true && x.Exam.IsTest == false && x.Exam.IsCanceled == false).Count()));
    }
}
