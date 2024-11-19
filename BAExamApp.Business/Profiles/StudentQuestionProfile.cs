using BAExamApp.Dtos.StudentQuestions;

namespace BAExamApp.Business.Profiles;
public class StudentQuestionProfile : Profile
{
    public StudentQuestionProfile()
    {
        CreateMap<StudentQuestion, StudentQuestionDto>();
        CreateMap<StudentQuestion, StudentQuestionListDto>();
        CreateMap<StudentQuestion, StudentQuestionDetailsDto>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Question.Content))
            .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.Question.QuestionType))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Question.Image))
            .ForMember(dest => dest.TimeGiven, opt => opt.MapFrom(src => src.Question.QuestionDifficulty.TimeGiven))
            .ForMember(dest => dest.QuestionAnswers, opt => opt.MapFrom(src => src.Question.QuestionAnswers))
            .ForMember(dest => dest.ExamDuration, opt => opt.MapFrom(src => src.StudentExam.Exam.ExamDuration))
            .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.StudentExam.Exam.ExamDateTime));

        CreateMap<StudentQuestionCreateDto, StudentQuestion>();
        CreateMap<StudentQuestionUpdateDto, StudentQuestion>();
    }
}
