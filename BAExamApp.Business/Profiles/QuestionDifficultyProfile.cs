using BAExamApp.Dtos.QuestionDifficulty;

namespace BAExamApp.Business.Profiles;
public class QuestionDifficultyProfile : Profile
{
    public QuestionDifficultyProfile()
    {
        CreateMap<QuestionDifficulty, QuestionDifficultyDto>();
        CreateMap<QuestionDifficulty, QuestionDifficultyListDto>();
        CreateMap<QuestionDifficultyCreateDto, QuestionDifficulty>();
        CreateMap<QuestionDifficultyUpdateDto, QuestionDifficulty>();
    }
}
