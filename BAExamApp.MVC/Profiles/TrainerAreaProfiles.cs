using AutoMapper;
using BAExamApp.Dtos.Classrooms;
using BAExamApp.Dtos.Dashboard;
using BAExamApp.Dtos.ExamClassrooms;
using BAExamApp.Dtos.ExamEvaluators;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Dtos.QuestionRevisions;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.QuestionSubtopics;
using BAExamApp.Dtos.StudentClassrooms;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Dtos.Students;
using BAExamApp.Dtos.Trainers;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Trainer.Models.ClassroomVMs;
using BAExamApp.MVC.Areas.Trainer.Models.DashboardVMs;
using BAExamApp.MVC.Areas.Trainer.Models.ExamEvaluatorVMs;
using BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionAnswerVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionRevisionVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;
using BAExamApp.MVC.Areas.Trainer.Models.StudentClassroomVMs;
using BAExamApp.MVC.Areas.Trainer.Models.StudentExamVMs;
using BAExamApp.MVC.Areas.Trainer.Models.StudentVMs;
using BAExamApp.MVC.Areas.Trainer.Models.TrainerVMs;

namespace BAExamApp.MVC.Profiles
{
    public class TrainerAreaProfiles : Profile
    {
        public TrainerAreaProfiles()
        {
            //Question Controller

            //List
            CreateMap<QuestionListDto, TrainerQuestionListVM>().ForMember(dest => dest.SubtopicName, opt => opt.MapFrom(src => src.SubtopicName));
            CreateMap<QuestionRevisionListDto, TrainerQuestionRevisionListVM>();

            //Create
            CreateMap<TrainerQuestionCreateVM, QuestionCreateDto>().ForMember(dest => dest.SubtopicId, opt => opt.MapFrom(src => src.SubtopicId));
            CreateMap<TrainerQuestionAnswerCreateVM, QuestionAnswerCreateDto>();
            CreateMap<QuestionDto, TrainerQuestionCreateVM>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.SubtopicId, opt => opt.MapFrom(src => src.SubtopicId.Select(subtopicDto => subtopicDto.Id).ToList()));
            CreateMap<QuestionAnswerDto, TrainerQuestionAnswerCreateVM>();

            //Details
            CreateMap<QuestionDetailsDto, TrainerQuestionDetailsVM>()
                    .ForMember(dest => dest.SubtopicName, opt => opt.MapFrom(src => src.SubtopicName))
                    .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.SubjectName))
                    .ForMember(dest => dest.CommonSubjectAndSubtopics, opt => opt.Ignore())
                    .ForMember(dest => dest.RejectComment, opt => opt.MapFrom(src => src.RejectComment))
                    .AfterMap((src, dest) =>
                    {
                        dest.CommonSubjectAndSubtopics = dest.SubtopicName
                            .GroupBy(subtopic => dest.SubjectName[dest.SubtopicName.IndexOf(subtopic)])
                            .ToDictionary(
                                grouping => grouping.Key,
                                grouping => grouping.ToList()
                            );
                    });

            //Update
            CreateMap<QuestionDto, TrainerQuestionUpdateVM>()
                .ForMember(dest => dest.SubtopicId, opt => opt.MapFrom(src => src.SubtopicId.Select(subtopicDto => subtopicDto.SubtopicId)));
            CreateMap<QuestionAnswerDto, TrainerQuestionAnswerUpdateVM>();
            CreateMap<TrainerQuestionUpdateVM, QuestionUpdateDto>()
                .ForMember(dest => dest.SubtopicId, opt => opt.MapFrom(src => src.SubtopicId));
            CreateMap<TrainerQuestionAnswerUpdateVM, QuestionAnswerUpdateDto>();
            CreateMap<QuestionUpdateDto, Question>()
                .ForMember(dest => dest.QuestionSubtopics, opt => opt.MapFrom(src => src.SubtopicId));

            //Classroom Controller

            //List
            CreateMap<ClassroomListDto, TrainerClassroomListVM>();
            CreateMap<ClassroomDetailsDto, TrainerClassroomDetailsVM>();

            //Student Controller

            //Details
            CreateMap<StudentDetailsForTrainerDto, TrainerStudentDetailsVM>();

            CreateMap<StudentExamsDetailsDto, StudentExamsForTrainerVM>();

            //StudentClassroom Details
            CreateMap<StudentClassroomListForClassroomDetailsDto, TrainerStudentClassroomListForClassroomDetailsVM>();

            //StudentExam List
            CreateMap<StudentExamListForTrainerDto, TrainerStudentExamListVM>();
            CreateMap<StudentExamListDto, StudentExamDetailForTrainerVM>();
            CreateMap<DashboardTopRatedStudentDto, StudentExamsForTrainerVM>().ReverseMap();
            CreateMap<DashboardTopRatedStudentDto, AllStudentExamForTrainerVM>().ReverseMap();
            CreateMap<DashboardTopRatedStudentDto, ActiveStudentExamForTrinerVM>().ReverseMap();

            //The mapping process has been carried out to list the excuses of students who did not take the exam
            CreateMap<StudentExamListDto, StudentExamStatusForTrainerVM>();

            //Trainer Controller

            //Details
            CreateMap<TrainerDetailsForTrainerDto, TrainerTrainerDetailVM>();

            //Update
            CreateMap<TrainerDto, TrainerTrainerUpdateVM>();
            CreateMap<TrainerTrainerUpdateVM, TrainerUpdateDto>();

            //Exam
            CreateMap<ExamListDto, TrainerExamListVM>()
                .ForMember(dest => dest.ClassroomName, opt => opt.MapFrom(src => String.Join(", ", src.ClassroomNames)));
            CreateMap<ExamDetailDto, TrainerExamDetailVM>();
            CreateMap<ExamListDto, TrainerExamListVM>();


            //Exam Update
            CreateMap<ExamDto, TrainerExamUpdateVM>();
            CreateMap<TrainerExamUpdateVM, ExamUpdateDto>();
            CreateMap<ExamUpdateDto, Exam>();

            CreateMap<TrainerExamCreateVM, ExamCreateDto>()
                .ForMember(dest => dest.ExamClassroomsIds, opt => opt.MapFrom(src => src.ExamClassroomsIds))
                .ForMember(dest => dest.ExamType, opt => opt.MapFrom(src => src.ExamType));


            //ExamEvaluators List ***** ExamRule'a sınav tipi eklendikten sonra eklenecek. *****
            CreateMap<ExamEvaluatorListDto, TrainerExamEvaluatorListVM>();
            CreateMap<ExamStrudentQuestionDetailsDto, TrainerExamStudentQuestionDetailsVM>().ReverseMap();


            //Home Controller

            //Index 
            CreateMap<DashboardEventDto, DashboardEventVM>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.ExamDateTime))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.ExamDateTime.Add(src.ExamDuration)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)).ReverseMap();

            CreateMap<DashboardTrainerQuestionDto, DashboardQuestionVM>().ReverseMap();
            CreateMap<DashboardQuestionVM, Areas.Trainer.Models.DashboardVMs.TrainerDashboardVM>().ReverseMap();
        }
    }
}
