using BAExamApp.Dtos.ExamRuleSubtopics;

namespace BAExamApp.Business.Profiles;

public class ExamRuleSubtopicProfile : Profile
{
    public ExamRuleSubtopicProfile()
    {
        CreateMap<ExamRuleSubtopic, ExamRuleSubtopicDto>()
            .ForMember(dest => dest.SubjectId, config => config.MapFrom(x => x.Subtopic.SubjectId));
        CreateMap<ExamRuleSubtopic, ExamRuleSubtopicDetailDto>()
            .ForMember(dest => dest.QuestionDifficultyName, config => config.MapFrom(x => x.QuestionDifficulty.Name))
            .ForMember(dest => dest.SubtopicName, config => config.MapFrom(x => x.Subtopic.Name))
            .ForMember(dest => dest.SubjectName, config => config.MapFrom(x => x.Subtopic.Subject.Name));
        CreateMap<ExamRuleSubtopicCreateDto, ExamRuleSubtopic>();
        CreateMap<ExamRuleSubtopicUpdateDto, ExamRuleSubtopic>();
    }
}
