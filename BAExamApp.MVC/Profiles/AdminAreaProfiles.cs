using AutoMapper;
using BAExamApp.Dtos.Admins;
using BAExamApp.Dtos.ApiUsers;
using BAExamApp.Dtos.Branches;
using BAExamApp.Dtos.Candidate.CandidateAdmins;
using BAExamApp.Dtos.Classrooms;
using BAExamApp.Dtos.Dashboard;
using BAExamApp.Dtos.Emails;
using BAExamApp.Dtos.ExamRules;
using BAExamApp.Dtos.ExamRuleSubtopics;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.GroupTypes;
using BAExamApp.Dtos.Products;
using BAExamApp.Dtos.ProductSubjects;
using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Dtos.QuestionArranges;
using BAExamApp.Dtos.QuestionDifficulty;
using BAExamApp.Dtos.QuestionRevisions;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.SentMails;
using BAExamApp.Dtos.StudentAnswers;
using BAExamApp.Dtos.StudentClassrooms;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Dtos.StudentQuestions;
using BAExamApp.Dtos.Students;
using BAExamApp.Dtos.Subjects;
using BAExamApp.Dtos.Subtopics;
using BAExamApp.Dtos.TecnicalUnits;
using BAExamApp.Dtos.TrainerClassrooms;
using BAExamApp.Dtos.TrainerProducts;
using BAExamApp.Dtos.Trainers;
using BAExamApp.Entities.DbSets;
using BAExamApp.MVC.Areas.Admin.Models.AdminVMs;
using BAExamApp.MVC.Areas.Admin.Models.ApiUserVMs;
using BAExamApp.MVC.Areas.Admin.Models.BranchVMs;
//using BAExamApp.MVC.Areas.Admin.Models.CityVMs;
using BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;
using BAExamApp.MVC.Areas.Admin.Models.DashboardVMs;
using BAExamApp.MVC.Areas.Admin.Models.EmailVMs;
using BAExamApp.MVC.Areas.Admin.Models.ExamEvaluatorVMs;
using BAExamApp.MVC.Areas.Admin.Models.ExamRuleSubtopicVMs;
using BAExamApp.MVC.Areas.Admin.Models.ExamRuleVMs;
using BAExamApp.MVC.Areas.Admin.Models.ExamVMs;
using BAExamApp.MVC.Areas.Admin.Models.GroupTypeVMs;
using BAExamApp.MVC.Areas.Admin.Models.ProductVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionAnswerVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionArrangeVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionDifficultyVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionRevisionVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;
using BAExamApp.MVC.Areas.Admin.Models.SentMailVMs;
using BAExamApp.MVC.Areas.Admin.Models.StudentAnswerVMs;
using BAExamApp.MVC.Areas.Admin.Models.StudentQuestion;
using BAExamApp.MVC.Areas.Admin.Models.StudentVMs;
using BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;
using BAExamApp.MVC.Areas.Admin.Models.SubtopicVMs;
using BAExamApp.MVC.Areas.Admin.Models.TechnicalUnitVMs;
using BAExamApp.MVC.Areas.Admin.Models.TrainerVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateAdminVMs;
using BAExamApp.MVC.Areas.Student.Models.StudentVMs;
using BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionAnswerVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;
using BAExamApp.MVC.Areas.Trainer.Models.StudentExamVMs;

namespace BAExamApp.MVC.Profiles;

public class AdminAreaProfiles : Profile
{
    public AdminAreaProfiles()
    {
        //AdminController
        CreateMap<AdminListDto, AdminAdminListVM>();
        CreateMap<AdminDto, AdminAdminUpdateVM>();
        CreateMap<AdminAdminCreateVM, AdminCreateDto>();
        CreateMap<AdminDetailsDto, AdminAdminDetailsVM>();
        CreateMap<AdminAdminUpdateVM, AdminUpdateDto>();
        CreateMap<AdminDto, AdminAdminListVM>();

        //BranchController
        CreateMap<BranchListDto, AdminBranchListVM>();
        CreateMap<AdminBranchCreateVM, BranchCreateDto>();
        CreateMap<BranchDto, AdminBranchUpdateVM>();
        CreateMap<BranchDto, AdminBranchDetailsVM>();
        CreateMap<AdminBranchUpdateVM, BranchUpdateDto>();
        CreateMap<BranchDetailsDto, AdminBranchDetailsVM>();

        //CandidateAdminController
        CreateMap<CandidateAdminCreateVM, CandidateAdminCreateDto>();
        CreateMap<CandidateAdminListDto, CandidateAdminListVM>();
        CreateMap<CandidateAdminDetailsDto, CandidateAdminDetailsVM>();
        CreateMap<CandidateAdminUpdateDto, CandidateAdminUpdateVM>();
        CreateMap<CandidateAdminUpdateVM, CandidateAdminUpdateDto>();
        CreateMap<CandidateAdminDto, CandidateAdminUpdateDto>();
        CreateMap<CandidateAdminUpdateVM, CandidateAdminDto>();


        //ClassroomController
        CreateMap<ClassroomListDto, AdminClassroomListVM>();
        CreateMap<ClassroomDetailsForAdminDto, AdminClassroomDetailsVM>();
        CreateMap<ExamListDto, AdminClassroomDetailsVM>();
        CreateMap<ClassroomDetailsForAdminDto, AdminClassroomUpdateVM>();
        CreateMap<AdminClassroomCreateVM, ClassroomCreateDto>();
        CreateMap<ClassroomDto, AdminClassroomUpdateVM>();
        CreateMap<AdminClassroomUpdateVM, ClassroomUpdateDto>();
        CreateMap<AdminClassroomAddTrainerVM, TraninerAddClassroomDto>();
        CreateMap<AdminClassroomAddStudentVM, StudentAddToClassroomDto>();

        //ExamController
        CreateMap<ExamListDto, AdminExamListVM>();
        CreateMap<ExamDetailDto, AdminExamDetailVM>()
     .ForMember(dest => dest.ExamsEvaluators, opt => opt.MapFrom(src => src.ExamEvaluators));
        CreateMap<StudentExamListDto, StudentExamDetailForAdminVM>();
        CreateMap<StudentExamsAdminDto, StudentExamsForAdminVM>();
        CreateMap<AdminExamCreateVM, ExamCreateDto>();
        CreateMap<ExamStrudentQuestionDetailsDto, AdminExamStudentQuestionDetailsVM>();
        CreateMap<ExamDto, AdminExamUpdateVM>().ReverseMap();
        CreateMap<AdminExamUpdateVM, ExamUpdateDto>().ReverseMap();
        CreateMap<StudentExamsAdminDto, ActiveStudentsExamsForAdminVM>().ReverseMap();

        //ExamEvaluatorController
        CreateMap<ExamDetailDto, AdminExamEvaluatorCreateVM>();

        //ExamRuleController
        CreateMap<ExamRuleListDto, AdminExamRuleListVM>();
        CreateMap<AdminExamRuleCreateVM, ExamRuleCreateDto>();
        CreateMap<AdminExamRuleSubtopicCreateVM, ExamRuleSubtopicCreateDto>();
        CreateMap<ExamRuleDto, AdminExamRuleUpdateVM>();
        CreateMap<ExamRuleSubtopicDto, AdminExamRuleSubtopicUpdateVM>();
        CreateMap<AdminExamRuleUpdateVM, ExamRuleUpdateDto>();
        CreateMap<AdminExamRuleSubtopicUpdateVM, ExamRuleSubtopicUpdateDto>();
        CreateMap<ExamRuleDetailsDto, AdminExamRuleDetailsVM>();
        CreateMap<ExamRuleSubtopicDetailDto, AdminExamRuleSubtopicDetailVM>();

        //GroupTypeController
        CreateMap<GroupTypeListDto, AdminGroupTypeListVM>();
        CreateMap<AdminGroupTypeCreateVM, GroupTypeCreateDto>();
        CreateMap<GroupTypeDto, AdminGroupTypeDetailVM>();
        CreateMap<GroupTypeDto, AdminGroupTypeUpdateVM>();
        CreateMap<GroupTypeDto, AdminGroupTypeUpdateVM>();
        CreateMap<GroupTypeUpdateDto, AdminGroupTypeUpdateVM>();
        CreateMap<AdminGroupTypeUpdateVM, GroupTypeUpdateDto>();

        //ProductController
        CreateMap<ProductListDto, AdminProductListVM>();
        CreateMap<AdminProductCreateVM, ProductCreateDto>();
        CreateMap<ProductDetailDto, AdminProductDetailVM>();
        CreateMap<ProductDto, AdminProductUpdateVM>()
            .ForMember(dest => dest.SubjectIds, opt => opt.MapFrom(src => src.ProductSubjects.Select(x => x.SubjectId)));
        CreateMap<AdminProductUpdateVM, ProductUpdateDto>();
        CreateMap<TrainerProductListForProductDetailsDto, AdminProductTrainerVM>();
        CreateMap<ProductSubjectListDto, AdminSubjectListVM>();
        CreateMap<AdminProductAddTrainerVM, ProductAddTrainerDto>();

        //QuestionController
        CreateMap<QuestionListDto, AdminQuestionListVM>().ForMember(dest => dest.SubtopicName, opt => opt.MapFrom(src => src.SubtopicName));
        CreateMap<QuestionDetailsForAdminDto, AdminQuestionDetailsVM>();
        CreateMap<QuestionDetailsForAdminDto, AdminQuestionReviewVM>();
        CreateMap<QuestionDetailsForAdminDto, QuestionRevisionCreateDto>();
        CreateMap<QuestionRevisionListDto, QuestionRevisionListVM>();
        CreateMap<QuestionDetailForAdminDto,AdminQuestionExamDetailsVM>();
        CreateMap<StudentAnswerDetailDto,AdminStudentAnswerDetailVM>();
        CreateMap<AdminQuestionDetailsQuestionAnswerVM, QuestionAnswerDto>();
        CreateMap<QuestionDetailsForAdminDto, AdminQuestionApprovedVM>();
        CreateMap<QuestionArrangeListDto, QuestionArrangeListVM>();
        CreateMap<AdminQuestionCreateVM, QuestionCreateDto>();
        CreateMap<AdminQuestionCreateVM, QuestionAnswerCreateDto>();
     

        CreateMap<QuestionDto, AdminQuestionUpdateVM>()
                .ForMember(dest => dest.SubtopicId, opt => opt.MapFrom(src => src.SubtopicId.Select(subtopicDto => subtopicDto.SubtopicId)));
        CreateMap<QuestionAnswerDto, TrainerQuestionAnswerUpdateVM>();
        CreateMap<AdminQuestionUpdateVM, QuestionUpdateDto>()
            .ForMember(dest => dest.SubtopicId, opt => opt.MapFrom(src => src.SubtopicId));
        CreateMap<TrainerQuestionAnswerUpdateVM, QuestionAnswerUpdateDto>();
        CreateMap<QuestionUpdateDto, Question>()
            .ForMember(dest => dest.QuestionSubtopics, opt => opt.MapFrom(src => src.SubtopicId));
        CreateMap<TrainerQuestionAnswerUpdateVM, QuestionAnswerDto>().ReverseMap();
        CreateMap<TrainerQuestionAnswerCreateVM, QuestionAnswerDto>().ReverseMap();
        CreateMap<AdminQuestionUpdateVM, QuestionArrangeCreateDto>()
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.ArrangeComment)).ReverseMap();

        //QuestionDifficultyController
        CreateMap<QuestionDifficultyCreateDto, AdminQuestionDifficultyCreateVM>().ReverseMap();
        CreateMap<QuestionDifficultyListDto, AdminQuestionDifficultyListVM>().ReverseMap();
        CreateMap<QuestionDifficultyDto, AdminQuestionDifficultyDetailVM>().ReverseMap();
        CreateMap<QuestionDifficultyUpdateDto, AdminQuestionDifficultyUpdateVM>().ReverseMap();
        CreateMap<QuestionDifficultyDto, AdminQuestionDifficultyUpdateVM>().ReverseMap();

        //StudentController
        CreateMap<StudentListDto, AdminStudentListVM>();
        CreateMap<StudentDto, AdminStudentUpdateVM>();
        CreateMap<AdminStudentCreateVM, StudentCreateDto>();
        CreateMap<StudentDetailsDto, AdminStudentDetailVM>();
        CreateMap<AdminStudentUpdateVM, StudentUpdateDto>();
        CreateMap<StudentExamsDetailsDto, StudentExamsForAdminVM>();
        CreateMap<StudentExamQuestionDto,AdminStudentExamQuestionVm>();
        CreateMap<SentMailListDto, AdminStudentMailListVM>();

        //StudentExam
        CreateMap<StudentExamDto ,StudentExamDetailForAdminVM>();
        CreateMap<QuestionAnswerDto,AdminQuestionDetailsQuestionAnswerVM>();
        

        //SubjectController
        CreateMap<AdminSubjectCreateVM, SubjectCreateDto>();
        CreateMap<SubjectListDto, AdminSubjectListVM>();
        CreateMap<SubjectDto, AdminSubjectUpdateVM>()
         .ForMember(dest => dest.ProductIds, opt => opt.MapFrom(src => src.ProductSubjects.Select(x => x.ProductId)));
        CreateMap<AdminSubjectUpdateVM, SubjectUpdateDto>();
        CreateMap<SubjectUpdateDto, AdminSubjectUpdateVM>();
        CreateMap<SubjectDetailDto, AdminSubjectDetailVM>();

        //SubtopicController
        CreateMap<AdminSubtopicCreateVm, SubtopicCreateDto>().ReverseMap();
        CreateMap<SubtopicListDto, AdminSubtopicListVM>().ReverseMap();
        CreateMap<SubtopicDto, AdminSubtopicUpdateVM>().ReverseMap();
        CreateMap<AdminSubtopicUpdateVM, SubtopicUpdateDto>().ReverseMap();
        CreateMap<SubtopicUpdateDto, AdminSubtopicUpdateVM>().ReverseMap();
        CreateMap<SubtopicDetailDto, AdminSubtopicDetailVM>().ReverseMap();

        //TechnicalUnitController
        CreateMap<TechnicalUnitListDto, AdminTechnicalUnitListVM>();
        CreateMap<AdminTechnicalUnitCreateVM, TechnicalUnitCreateDto>();
        CreateMap<TechnicalUnitDto, AdminTechnicalUnitDetailsVM>();
        CreateMap<TechnicalUnitDto, AdminTechnicalUnitUpdateVM>();
        CreateMap<AdminTechnicalUnitUpdateVM, TechnicalUnitUpdateDto>();

        //TrainerController
        CreateMap<TrainerListDto, AdminTrainerListVM>();
        CreateMap<TrainerListDto, AdminTrainerDetailsVM>();
        CreateMap<TrainerDto, AdminTrainerDetailsVM>();
        CreateMap<TrainerDetailsDto, AdminTrainerDetailsVM>();
        CreateMap<AdminTrainerCreateVM, TrainerCreateDto>();
        CreateMap<AdminTrainerUpdateVM, TrainerUpdateDto>();
        CreateMap<TrainerDto, AdminTrainerUpdateVM>();

        //User Controller
        CreateMap<TrainerDto, TrainerListDto>();

        //Admin,Trainer,StudentController
        CreateMap<EmailCreateDto, AdminEmailCreateVM>().ReverseMap();
        CreateMap<EmailCreateDto, AdminAdminUpdateVM>().ReverseMap();

        //HomeController
        CreateMap<DasboardOverviewDTO, DashboardOverviewVM>().ReverseMap();
        CreateMap<DashboardTopRatedStudentDto, StudentExamsForAdminVM>().ReverseMap();
        CreateMap<DashboardTopRatedStudentDto, StudentExamsForTrainerVM>().ReverseMap();
        CreateMap<DashboardTopRatedStudentDto, AllStudentExamForTrainerVM>().ReverseMap();
        
        CreateMap<DashboardEventDto, DashboardEventVM>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.ExamDateTime))
            .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.ExamDateTime.Add(src.ExamDuration)))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        
        //ApiUserController
        CreateMap<ApiUserListDto, AdminApiUserListVM>();
        CreateMap<CreateApiUserDto, AdminApiUserCreateVM>().ReverseMap();
        CreateMap<UpdateApiUserDto, AdminApiUserUpdateVM>().ReverseMap();
        CreateMap<ApiUserDto, AdminApiUserDetailsVM>();
        CreateMap<ApiUserDto, AdminApiUserUpdateVM>();
    }
}