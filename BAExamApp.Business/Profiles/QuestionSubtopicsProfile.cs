using BAExamApp.Dtos.QuestionSubtopics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Profiles;
public class QuestionSubtopicsProfile : Profile
{
    public QuestionSubtopicsProfile()
    {
        CreateMap<Guid, QuestionSubtopicsCreateDto>();
        CreateMap<Guid, QuestionSubtopicsUpdateDto>();
        CreateMap<QuestionSubtopicsCreateDto, QuestionSubtopics>();
        CreateMap<QuestionSubtopicsUpdateDto, QuestionSubtopics>();
    }
}
