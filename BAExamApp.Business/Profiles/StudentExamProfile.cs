using BAExamApp.Dtos.ApiDtos.StudentExamApiDtos;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.StudentAnswers;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Dtos.StudentQuestions;

namespace BAExamApp.Business.Profiles;

public class StudentExamProfile : Profile
{
    public StudentExamProfile()
    {
        CreateMap<StudentExam, StudentExamDto>()
            .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.FullName));

        CreateMap<StudentExam, StudentExamListDto>()
            .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
            .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.Exam.ExamDateTime))
            .ForMember(dest => dest.ExamDuration, opt => opt.MapFrom(src => src.Exam.ExamDuration))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
            .ForMember(dest => dest.EvaluatorName, opt => opt.MapFrom(src => src.Exam.ExamEvaluators.FirstOrDefault().Trainer.FullName))
            .ForMember(dest => dest.MaxScore, opt => opt.MapFrom(src => src.Exam.MaxScore))
            .ForMember(dest => dest.ClassroomNames, opt => opt.MapFrom(src => src.Student.StudentClassrooms.Select(ec => ec.Classroom.Name).Distinct().ToList()));

        CreateMap<StudentExam, StudentExamListForTrainerDto>()
            .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
            .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.Exam.ExamDateTime))
            .ForMember(dest => dest.MaxScore, opt => opt.MapFrom(src => src.Exam.MaxScore));


        CreateMap<StudentExam, StudentExamsAdminDto>()
            .ForMember(dest => dest.ClassroomNames, opt => opt.MapFrom(src => src.Exam.ExamClassrooms.Select(ec => ec.Classroom.Name).ToList()))
            .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
            .ForMember(dest => dest.MaxScore, opt => opt.MapFrom(src => src.Exam.MaxScore))
            .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.Exam.ExamDateTime))
            .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.FullName));

        CreateMap<StudentExam, StudentExamsDetailsDto>()
            .ForMember(dest => dest.ClassroomNames, opt => opt.MapFrom(src => src.Exam.ExamClassrooms.Select(ec => ec.Classroom.Name).ToList()))
            .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
            .ForMember(dest => dest.MaxScore, opt => opt.MapFrom(src => src.Exam.MaxScore))
            .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.Exam.ExamDateTime))
            .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.FullName))
            .ForMember(dest => dest.StudentClassroomNames, opt => opt.MapFrom(src => src.Student.StudentClassrooms.Select(ec => ec.Classroom.Name).Distinct().ToList()))
            .ForMember(dest => dest.ExamType, opt => opt.MapFrom(src => src.Exam.ExamType))
            .ForMember(dest => dest.IsCanceled, opt => opt.MapFrom(src => src.Exam.IsCanceled))
            .ForMember(dest => dest.IsTest, opt => opt.MapFrom(src => src.Exam.IsTest));


        CreateMap<StudentQuestion,StudentExamQuestionDto>();
        CreateMap<StudentAnswer,StudentAnswerDetailDto>()
            .ForMember(dest=>dest.IsCorrect,opt=>opt.MapFrom(src=>src.QuestionAnswer.IsRightAnswer));

        CreateMap<StudentExamCreateDto, StudentExam>();
        CreateMap<StudentExamUpdateDto, StudentExam>();
        CreateMap<StudentExam, ExamStrudentQuestionDetailsDto>()
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => string.Concat(src.Student.FirstName, " ", src.Student.LastName)))
            .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
            .ForMember(dest => dest.MaxScore, opt => opt.MapFrom(src => src.Exam.MaxScore))
            .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
            .ForMember(dest=>dest.ClassroomNames,opt=>opt.MapFrom(src=>src.Student.StudentClassrooms.Select(ec=>ec.Classroom.Name)));

        //Api
        CreateMap<StudentExam, StudentExamListApiDto>()
        .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Student.FullName))
        .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score))
        .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name));

    }
}
