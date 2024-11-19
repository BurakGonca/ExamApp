using BAExamApp.Dtos.Subtopics;

namespace BAExamApp.Business.Profiles;
public class SubtopicProfile : Profile
{
    public SubtopicProfile()
    {
        CreateMap<Subtopic, SubtopicListDto>();
        CreateMap<SubtopicCreateDto, Subtopic>().ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name.Trim()));
        CreateMap<Subtopic, SubtopicDto>();
        CreateMap<Subtopic, SubtopicDetailDto>();
        CreateMap<SubtopicUpdateDto, Subtopic>();
    }
}
