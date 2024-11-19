using BAExamApp.Dtos.Emails;

namespace BAExamApp.Business.Profiles;

public class EmailProfile : Profile
{
    public EmailProfile()
    {
        CreateMap<Email, EmailDto>();
        CreateMap<EmailCreateDto, Email>();
        CreateMap<EmailUpdateDto, Email>();
    }
}
