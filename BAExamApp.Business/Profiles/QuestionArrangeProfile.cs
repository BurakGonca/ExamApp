using BAExamApp.Dtos.QuestionArranges;

namespace BAExamApp.Business.Profiles;
internal class QuestionArrangeProfile : Profile
{
    public QuestionArrangeProfile()
    {
        CreateMap<QuestionArrange, QuestionArrangeCreateDto>().ReverseMap();
        CreateMap<QuestionArrangeDto, QuestionArrangeCreateDto>().ReverseMap();
        CreateMap<QuestionArrangeListDto, QuestionArrange>().ReverseMap();
    }
}
