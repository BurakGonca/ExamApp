using BAExamApp.Dtos.Admins;
using Microsoft.AspNetCore.Http;

namespace BAExamApp.Business.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<Admin, AdminDto>()
            .ForMember(dest => dest.OriginalImage, opt => opt.MapFrom(src => src.NewImage));
        CreateMap<Admin, AdminListDto>();
        CreateMap<AdminCreateDto, Admin>()
            .ForMember(dest => dest.NewImage, opt => opt.MapFrom(src => src.NewImage != null ? ConvertIFormFileToByteArray(src.NewImage) : null));
        CreateMap<AdminUpdateDto, Admin>()
            .ForMember(dest => dest.NewImage, opt => opt.Condition(src => src.RemoveImage == true || (src.NewImage != null && src.RemoveImage == false)))
            .ForMember(dest => dest.NewImage, opt => opt.MapFrom(src => ConvertIFormFileToByteArray(src.NewImage)));
        CreateMap<Admin, AdminDetailsDto>();/*.ForMember(dest => dest.CityName,config => config.MapFrom(src => src.City.Name));*/
        CreateMap<AdminDto, AdminUpdateDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            // Diğer üyeler için aynı şekilde devam edin.
            .ReverseMap();
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