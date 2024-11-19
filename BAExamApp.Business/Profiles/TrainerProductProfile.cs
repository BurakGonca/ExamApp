using BAExamApp.Dtos.TrainerProducts;

namespace BAExamApp.Business.Profiles;
public class TrainerProductProfile : Profile
{
    public TrainerProductProfile()
    {
        CreateMap<TrainerProduct, TrainerProductListDto>()
            .ForMember(destination => destination.TrainerId, config => config.MapFrom(src => src.Trainer.Id))
            .ForMember(destination => destination.FullName, config => config.MapFrom(src => src.Trainer.FullName));
        CreateMap<TrainerProduct, TrainerProductListForProductDetailsDto>()
            .ForMember(destination => destination.FullName, config => config.MapFrom(src => src.Trainer.FullName));
    }
}
