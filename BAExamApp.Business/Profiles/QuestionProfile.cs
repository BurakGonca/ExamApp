using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Dashboard;
using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.QuestionSubtopics;

namespace BAExamApp.Business.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        //CreateMap<Question, QuestionDto>();
        CreateMap<Question, QuestionDto>()
            .ForMember(dest => dest.SubtopicId, opt => opt.MapFrom(src => src.QuestionSubtopics.Select(x => new QuestionSubtopicsDto { SubtopicId = x.SubtopicId })));
        CreateMap<Question, QuestionListDto>()
            .ForMember(dest => dest.TimeGiven, config => config.MapFrom(src => src.QuestionDifficulty.TimeGiven))
            .ForMember(dest => dest.SubtopicName, config => config.MapFrom(src => src.QuestionSubtopics.Select(x => x.Subtopic.Name)))
            .ForMember(dest => dest.QuestionDifficultyName, config => config.MapFrom(src => src.QuestionDifficulty.Name));
        CreateMap<Question, QuestionDetailsDto>()         
            .ForMember(dest => dest.RejectComment, config => config.MapFrom(src => src.RejectComment))
            .ForMember(dest => dest.TimeGiven, config => config.MapFrom(src => src.QuestionDifficulty.TimeGiven))
            .ForMember(dest => dest.SubjectName, config => config.MapFrom(src => src.QuestionSubtopics.Where(x=>x.Status!=Status.Deleted).Select(x => x.Subtopic.Subject.Name)))
            .ForMember(dest => dest.SubtopicName, config => config.MapFrom(src => src.QuestionSubtopics.Select(x => x.Subtopic.Name)))
            .ForMember(dest => dest.QuestionDifficultyName, config => config.MapFrom(src => src.QuestionDifficulty.Name));
        CreateMap<Question, QuestionDetailsForAdminDto>()
            .ForMember(dest => dest.SubtopicName, config => config.MapFrom(src => src.QuestionSubtopics.Select(x => x.Subtopic.Name)))
            .ForMember(dest => dest.QuestionDifficultyName, config => config.MapFrom(src => src.QuestionDifficulty.Name))
            .ForMember(dest => dest.RequestDescription, config => config.MapFrom(src => src.QuestionRevisions.OrderByDescending(r => r.CreatedDate).FirstOrDefault().RequestDescription))
            .ForMember(dest => dest.RevisionConclusion, config => config.MapFrom(src => src.QuestionRevisions.OrderByDescending(r => r.CreatedDate).FirstOrDefault().RevisionConclusion))
            .ForMember(dest => dest.RequesterAdmin, config => config.MapFrom(src => src.QuestionRevisions.OrderByDescending(r => r.CreatedDate).FirstOrDefault().RequesterAdmin.FirstName + " " + src.QuestionRevisions.OrderByDescending(r => r.CreatedDate).FirstOrDefault().RequesterAdmin.LastName));

        CreateMap<QuestionCreateDto, Question>()
            .ForMember(dest => dest.QuestionSubtopics, opt => opt.MapFrom(src => src.SubtopicId));
        CreateMap<QuestionUpdateDto, Question>()
            .ForMember(dest => dest.QuestionSubtopics, opt => opt.MapFrom(src => src.SubtopicId)).ForMember(dest=>dest.QuestionAnswers,opt=>opt.MapFrom(src=>src.QuestionAnswers));
        CreateMap<QuestionCreateDto, QuestionDto>()
            .ForMember(dest => dest.SubtopicId, opt => opt.MapFrom(src => src.SubtopicId));
        CreateMap<QuestionUpdateDto, QuestionDto>()
            .ForMember(dest => dest.SubtopicId, opt => opt.MapFrom(src => src.SubtopicId));
        CreateMap<QuestionAnswerDto, QuestionDto>();
        
        CreateMap<Question,QuestionDetailForAdminDto>().ForMember(dest=>dest.SubtopicName,opt=>opt.MapFrom(q=>q.QuestionSubtopics.Where(x=>x.Status!=Status.Deleted).Select(x=>x.Subtopic.Name))).ForMember(dest=>dest.QuestionDifficultyName,opt=>opt.MapFrom(src=>src.QuestionDifficulty.Name));

        CreateMap<Question, DashboardTrainerQuestionDto>().ReverseMap();

    }
}
