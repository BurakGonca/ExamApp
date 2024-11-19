using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.TrainerClassrooms;

namespace BAExamApp.Business.Profiles;
public class TrainerClassroomProfile : Profile
{
    public TrainerClassroomProfile()
    {
        CreateMap<TrainerClassroomExamListDto, ExamListDto>().ReverseMap();
        CreateMap<TrainerClassromDto, Trainer>().ReverseMap();
        CreateMap<TrainerClassroom, TrainerClassromDto>()
      .ForMember(dest => dest.TrainerTalentIds, opt => opt.MapFrom(src => new List<Guid> { src.TrainerId }))
      .ReverseMap();


        CreateMap<TrainerClassroom, TrainerClassroomListForTrainerDetailsDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Classroom.Id))
            .ForMember(dest => dest.ClassroomName, opt => opt.MapFrom(src => src.Classroom.Name))
            .ForMember(dest => dest.OpeningDate, opt => opt.MapFrom(src => src.Classroom.OpeningDate))
            .ForMember(dest => dest.ClosedDate, opt => opt.MapFrom(src => src.Classroom.ClosedDate))
            .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Classroom.StudentClassrooms.Count));
        CreateMap<TrainerClassroom, TrainerClassroomListForClassroomDetailsDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Trainer.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Trainer.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Trainer.LastName));
    }
}
