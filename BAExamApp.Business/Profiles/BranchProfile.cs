using BAExamApp.Dtos.Branches;

namespace BAExamApp.Business.Profiles;

public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<Branch, BranchDto>();
        CreateMap<Branch, BranchListDto>();
        CreateMap<Branch, BranchDetailsDto>();
        CreateMap<BranchCreateDto, Branch>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()));
        CreateMap<BranchUpdateDto, Branch>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()));
    }
}
