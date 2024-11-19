using BAExamApp.Dtos.QuestionFeedbacks;

namespace BAExamApp.Business.Profiles;

public class QuestionFeedbackProfile : Profile
{
    public QuestionFeedbackProfile()
    {
        CreateMap<QuestionFeedback, QuestionFeedbackDto>();
        CreateMap<QuestionFeedbackCreateDto, QuestionFeedback>();
    }
}
