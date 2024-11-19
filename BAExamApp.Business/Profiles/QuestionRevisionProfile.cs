using BAExamApp.Dtos.QuestionRevisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Profiles;
public class QuestionRevisionProfile:Profile
{
    public QuestionRevisionProfile()
    {
        CreateMap<QuestionRevisionCreateDto, QuestionRevision>().ReverseMap();
        CreateMap<QuestionRevisionCreateDto, QuestionRevisionDto>().ReverseMap();
        CreateMap<QuestionRevision,QuestionRevisionListDto>()
            .ForMember(dest => dest.RequesterAdminName, opt => opt.MapFrom(src => src.RequesterAdmin.FullName))
            .ForMember(dest => dest.RequestedTrainerName, opt => opt.MapFrom(src => src.RequestedTrainer.FullName));
        CreateMap<QuestionRevisionUpdateDto, QuestionRevision>().ReverseMap();
        CreateMap<QuestionRevisionDto, QuestionRevision>().ReverseMap();
        CreateMap<QuestionRevisionDto, QuestionRevisionUpdateDto>().ReverseMap();
    }
}
