using BAExamApp.Dtos.ProductSubjects;

namespace BAExamApp.Business.Profiles;
public class ProductSubjectProfile : Profile
{
    public ProductSubjectProfile()
    {
        CreateMap<ProductSubject, ProductSubjectDto>().ReverseMap();
        CreateMap<ProductSubjectCreateDto, ProductSubject>();
        CreateMap<ProductSubject, ProductSubjectListDto>()
            .ForMember(dest=> dest.Name,opt=>opt.MapFrom(src=>src.Subject.Name));
    }
}
