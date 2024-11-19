using BAExamApp.Dtos.GroupTypes;

namespace BAExamApp.Business.Profiles;

public class GroupTypeProfile : Profile
{
    public GroupTypeProfile()
    {
        CreateMap<GroupType, GroupTypeDto>();
        CreateMap<GroupType, GroupTypeListDto>();
        CreateMap<GroupTypeCreateDto, GroupType>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()));
        CreateMap<GroupTypeUpdateDto, GroupType>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()));
    }
}