using BAExamApp.Dtos.ExamEvaluators;

namespace BAExamApp.Business.Profiles;
public class ExamEvaluatorProfile : Profile
{
    public ExamEvaluatorProfile()
    {
        CreateMap<ExamEvaluator, ExamEvaluatorDto>();
        CreateMap<ExamEvaluator, ExamEvaluatorListDto>()
            .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
            .ForMember(dest => dest.ClassroomNames, opt => opt.MapFrom(src => src.Exam.ExamClassrooms.Select(ec => ec.Classroom.Name).ToList()))
            .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.Exam.ExamDateTime))
            .ForMember(dest => dest.ExamDuration, opt => opt.MapFrom(src => src.Exam.ExamDuration));
        CreateMap<ExamEvaluatorCreateDto, ExamEvaluator>();
        CreateMap<ExamEvaluator, ExamEvaluatorListForExamDetailsDto>()
            .ForMember(dest => dest.EvaluatorId, opt => opt.MapFrom(src => src.Trainer.Id))
            .ForMember(dest => dest.EvaluatorName, opt => opt.MapFrom(src => src.Trainer.FullName));
    }
}
