using BAExamApp.Dtos.Products;

namespace BAExamApp.Business.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductListDto>();
        CreateMap<Product, ProductDetailDto>()
            .ForMember(dest => dest.ClassroomCount, opt => opt.MapFrom(src => src.ClassroomProducts.Count))
            .ForMember(dest => dest.TechnicalUnitName, opt => opt.MapFrom(src => src.TechnicalUnit!.Name));
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()));
        CreateMap<ProductUpdateDto, Product>();
    }
}
