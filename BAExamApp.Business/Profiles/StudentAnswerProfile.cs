using BAExamApp.Dtos.StudentAnswers;

namespace BAExamApp.Business.Profiles;
public class StudentAnswerProfile : Profile
{
    public StudentAnswerProfile()
    {
        CreateMap<StudentAnswer, StudentAnswerDto>();
        CreateMap<StudentAnswerCreateDto, StudentAnswer>();
        
        
    }
}
