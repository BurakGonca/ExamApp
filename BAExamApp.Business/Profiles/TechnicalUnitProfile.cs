using BAExamApp.Dtos.TecnicalUnits;

namespace BAExamApp.Business.Profiles;

public class TechnicalUnitProfile : Profile
{
    public TechnicalUnitProfile()
    {
        CreateMap<TechnicalUnit, TechnicalUnitDto>();
        CreateMap<TechnicalUnit, TechnicalUnitListDto>();

        CreateMap<TechnicalUnitCreateDto, TechnicalUnit>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()));

        CreateMap<TechnicalUnitUpdateDto, TechnicalUnit>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()));
    }
}
