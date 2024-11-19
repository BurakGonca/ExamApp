using BAExamApp.Dtos.Emails;
using BAExamApp.Dtos.SentMails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Profiles;
public class SentMailProfile : Profile
{
    public SentMailProfile()
    {
        CreateMap<SentMailCreateDto,SentMail>();
        CreateMap<SentMailUpdateDto,SentMail>().ReverseMap();
        CreateMap<SentMailDto, SentMailUpdateDto>().ReverseMap();
        CreateMap< SentMail, SentMailCreateDto>();
        CreateMap<SentMail, SentMailListDto>();
        CreateMap<SentMail, SentMailDto>();
        CreateMap<MailMessageDto,SentMailCreateDto>().ForMember(sentMail => sentMail.Email, mail => mail.MapFrom(dest=>dest.To));
        

    }
}
