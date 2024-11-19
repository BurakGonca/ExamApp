using BAExamApp.Dtos.ApiUsers;
using BAExamApp.Entities.ApiEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAExamApp.Business.Profiles
{
    public class ApiUserProfile : Profile
    {
        public ApiUserProfile()
        {
            CreateMap<ApiUser, ApiUserDto>().ForMember(dest => dest.OriginalImage, opt => opt.MapFrom(src => src.NewImage)); ;
            CreateMap<CreateApiUserDto, ApiUser>().ForMember(dest => dest.NewImage, opt => opt.MapFrom(src => src.NewImage != null ? ConvertIFormFileToByteArray(src.NewImage) : null)); ;
            CreateMap<UpdateApiUserDto, ApiUser>().ForMember(dest => dest.NewImage, opt => opt.Condition(src => src.RemoveImage == true || (src.NewImage != null && src.RemoveImage == false)))
            .ForMember(dest => dest.NewImage, opt => opt.MapFrom(src => ConvertIFormFileToByteArray(src.NewImage))); ;
            CreateMap<ApiUser, ApiUserListDto>().ReverseMap();
        }
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
}