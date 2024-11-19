using BAExamApp.Dtos.ExamClassrooms;

namespace BAExamApp.Business.Profiles
{
    public class ExamClassroomsProfile : Profile
    {
        public ExamClassroomsProfile()
        {
            CreateMap<ExamClassrooms, ExamClassroomsDto>();
            CreateMap<ExamClassrooms, ExamClassroomsListDto>()
                .ForMember(dest => dest.ClassroomName, opt => opt.MapFrom(src => src.Classroom.Name))
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name));
            CreateMap<Guid, ExamClassroomsCreateDto>();
            CreateMap<ExamClassroomsCreateDto, ExamClassrooms>();
            CreateMap<ExamClassrooms, ExamClassroomForClassroomDetailsDto>()
                .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.Exam.Id))
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
                .ForMember(dest => dest.ExamRule, opt => opt.MapFrom(src => src.Exam.ExamRule.Name))
                .ForMember(dest => dest.ExamType, opt => opt.MapFrom(src => src.Exam.ExamType))
                .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.Exam.ExamDateTime))
                .ForMember(dest => dest.ExamDuration, opt => opt.MapFrom(src => src.Exam.ExamDuration));
                

        }
    }
}
