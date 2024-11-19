using AutoMapper;
using BAExamApp.Dtos.ClassroomProducts;
using BAExamApp.Dtos.Classrooms;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.StudentAnswers;
using BAExamApp.Dtos.StudentClassrooms;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Dtos.StudentQuestions;
using BAExamApp.Dtos.Students;
using BAExamApp.MVC.Areas.Student.Models.ClassroomVMs;
using BAExamApp.MVC.Areas.Student.Models.ExamVMs;
using BAExamApp.MVC.Areas.Student.Models.ProductVMs;
using BAExamApp.MVC.Areas.Student.Models.StudentClassroomVMs;
using BAExamApp.MVC.Areas.Student.Models.StudentExamVMs;
using BAExamApp.MVC.Areas.Student.Models.StudentQuestionVMs;
using BAExamApp.MVC.Areas.Student.Models.StudentVMs;

namespace BAExamApp.MVC.Profiles
{
    public class StudentAreaProfiles : Profile
    {
        public StudentAreaProfiles()
        {
            //ClassroomController
            CreateMap<ClassroomProductListDto, StudentProductListVM>();
            CreateMap<ClassroomDetailsDto, StudentClassroomDetailsVM>();
            CreateMap<StudentClassroomListForStudentDto, StudentStudentClassroomListVM>();

            //StudentController
            CreateMap<StudentDto, StudentStudentDetailVM>();
            CreateMap<StudentDto, StudentStudentUpdateVM>();
            CreateMap<StudentStudentUpdateVM, StudentUpdateDto>();

            //ExamController
            CreateMap<StudentExamListDto, StudentExamListVM>();
            CreateMap<StudentExamDto, StudentStudentExamStartVM>()
                .ForMember(dest => dest.StudentExamId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.QuestionCount, opt => opt.MapFrom(src => src.StudentQuestions.Count));
            CreateMap<ExamDto, StudentStudentExamStartVM>()
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Name));
            CreateMap<StudentQuestionDetailsDto, StudentStudentQuestionDetailsVM>()
                .ForMember(dest => dest.StudentQuestionId, opt => opt.MapFrom(src => src.Id));
            CreateMap<StudentExamDto, StudentExamUpdateDto>();
            CreateMap<StudentQuestionDto, StudentQuestionUpdateDto>();
            CreateMap<StudentQuestionDetailsDto, StudentQuestionUpdateDto>();
            CreateMap<StudentAnswerDto, StudentAnswerCreateDto>();
            CreateMap<StudentExamDto, StudentStudentExamReportVM>()
                .ForMember(dest => dest.StudentExamId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.QuestionCount, opt => opt.MapFrom(src => src.StudentQuestions.Count));
            CreateMap<ExamDto, StudentStudentExamReportVM>()
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Name));
            CreateMap<StudentStudentExamStartVM, StudentStudentExamExcuseVM>();
        }
    }
}
