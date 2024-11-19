using BAExamApp.Dtos.ExamRules;

namespace BAExamApp.Business.Profiles;

public class ExamRuleProfile : Profile
{
    public ExamRuleProfile()
    {
        CreateMap<ExamRule, ExamRuleDto>();
        CreateMap<ExamRule, ExamRuleFilterDto>();
        CreateMap<ExamRule, ExamRuleListDto>();
        CreateMap<ExamRule, ExamRuleDetailsDto>()
            .ForMember(dest => dest.ProductName, config => config.MapFrom(src => src.Product.Name));
        CreateMap<ExamRuleCreateDto, ExamRule>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.Description, 
            config => config.MapFrom(src => src.Description.Trim()));
        CreateMap<ExamRuleUpdateDto, ExamRule>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.Description, 
            config => config.MapFrom(src => src.Description.Trim()));
    }
}