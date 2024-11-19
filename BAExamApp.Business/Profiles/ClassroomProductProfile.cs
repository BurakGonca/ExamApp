using BAExamApp.Dtos.ClassroomProducts;

namespace BAExamApp.Business.Profiles;
public class ClassroomProductProfile : Profile
{
    public ClassroomProductProfile()
    {
        CreateMap<ClassroomProduct, ClassroomProductDto>();
        CreateMap<ClassroomProduct, ClassroomProductListDto>()
            .ForMember(dest => dest.ClassroomName, opt => opt.MapFrom(src => src.Classroom.Name))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        CreateMap<ClassroomProductCreateDto, ClassroomProduct>();
    }
}
