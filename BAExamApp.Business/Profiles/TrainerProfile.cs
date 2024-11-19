using BAExamApp.Dtos.Admins;
using BAExamApp.Dtos.Trainers;
using Microsoft.AspNetCore.Http;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BAExamApp.Business.Profiles;

public class TrainerProfile : Profile
{
    public TrainerProfile()
    {
        CreateMap<Trainer, TrainerDto>()
            .ForMember(dest => dest.OriginalImage, opt => opt.MapFrom(src => src.NewImage))
            //.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.ProductIds, opt => opt.MapFrom(src => src.TrainerProducts.Select(x => x.ProductId)));
        CreateMap<Trainer, TrainerListDto>();
        CreateMap<Trainer, TrainerDetailsDto>();
        //.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
        CreateMap<Trainer, TrainerDetailsForTrainerDto>();
            //.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
        CreateMap<TrainerDto, Trainer>();
        CreateMap<TrainerCreateDto, Trainer>()
            .ForMember(dest => dest.NewImage, opt => opt.MapFrom(src => src.NewImage != null ? ConvertIFormFileToByteArray(src.NewImage) : null));
        CreateMap<TrainerUpdateDto, Trainer>()
       .ForMember(dest => dest.NewImage, opt => opt.Condition(src => src.RemoveImage == true || (src.NewImage != null && src.RemoveImage == false)))
       .ForMember(dest => dest.NewImage, opt => opt.MapFrom(src => ConvertIFormFileToByteArray(src.NewImage)));
        CreateMap<TrainerListDto, TrainerDto>();
    }

    /// <summary>
    /// Verilen IFormFile dosyasını byte dizisine dönüştürür.
    /// </summary>
    /// <param name="formFile">Dönüştürülecek IFormFile dosyası.</param>
    /// <returns>Byte dizisi olarak dönüştürülmüş dosya. Eğer dosya null veya boş ise null döner.</returns>
    private byte[] ConvertIFormFileToByteArray(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            formFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
   
}
