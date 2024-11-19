using BAExamApp.Business.ApiServices.Concrete;
using BAExamApp.Business.ApiServices.Interfaces;
using BAExamApp.Business.Interfaces.Services.Candidate;
using BAExamApp.Business.Profiles.MappingConfigurations;
using BAExamApp.Business.Services;
using BAExamApp.Business.Services.Candidate;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BAExamApp.Business.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        MappingConfigurations.MapsterConfig();

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ICandidateAdminService, CandidateAdminService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped<IClassroomService, ClassroomService>();
        services.AddScoped<IClassroomProductService, ClassroomProductService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IExamService, ExamService>();
        services.AddScoped<IExamEvaluatorService, ExamEvaluatorService>();
        services.AddScoped<IExamRuleService, ExamRuleService>();
        services.AddScoped<IExamRuleSubtopicService, ExamRuleSubtopicService>();
        services.AddScoped<IExamClassroomsService, ExamClassroomsService>();
        services.AddScoped<IGroupTypeService, GroupTypeService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductSubjectService, ProductSubjectService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
        services.AddScoped<IQuestionArrangeService, QuestionArrangeService>();
        services.AddScoped<IQuestionDifficultyService, QuestionDifficultyService>();
        services.AddScoped<IQuestionFeedbackService, QuestionFeedbackService>();
        services.AddScoped<IQuestionRevisionService, QuestionRevisionService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IStudentAnswerService, StudentAnswerService>();
        services.AddScoped<IStudentClassroomService, StudentClassroomService>();
        services.AddScoped<IStudentExamService, StudentExamService>();
        services.AddScoped<IStudentQuestionService, StudentQuestionService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<ISubtopicService, SubtopicService>();
        services.AddScoped<ITechnicalUnitService, TechnicalUnitService>();
        services.AddScoped<ITestExamService, TestExamService>();
        services.AddScoped<ITestExamQuestionService, TestExamQuestionService>();
        services.AddScoped<ITestExamTesterService, TestExamTesterService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ITrainerClassroomService, TrainerClassroomService>();
        services.AddScoped<ITrainerProductService, TrainerProductService>();
        services.AddScoped<IExamAnalysisService, ExamAnalysisService>();
        services.AddScoped<ISentMailService, SentMailService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<ICandidatesExamsService, CandidatesExamsService>();


        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ISendMailService, SendMailService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IObjectConvertFormatService, ObjectConvertFormatService>();

        services.AddScoped<ICandidateQuestionService, CandidateQuestionService>();
        services.AddScoped<ICandidateAnswerService, CandidateAnswerService>();
        services.AddScoped<ICandidateExamInitiationService, CandidateExamInitiationService>();

        services.AddScoped<ICandidateBranchService, CandidateBranchService>();

        services.AddScoped<ICandidateGroupService, CandidateGroupService>();
        services.AddScoped<ICandidateCandidatesGroupsService, CandidateCandidatesGroupsService>();
        services.AddScoped<ICandidateExamService, CandidateExamService>();

        services.AddScoped<IApiUserService, ApiUserService>();
        services.AddScoped<ICandidateExamEvaluationService, CandidateExamEvaluationService>();

        services.AddScoped<IStudentExamApiService, StudentExamApiService>();
        services.AddScoped<IStudentApiService, StudentApiService>();
        services.AddScoped<IRegisterCodeApiService, RegisterCodeApiService>();
        services.AddScoped<IExamApiService, ExamApiService>();


        services.AddScoped<ICandidateQuestionSubjectService, CandidateQuestionSubjectService>();
        services.AddScoped<ICandidateExamRuleService, CandidateExamRuleService>();
        services.AddScoped<ICandidateQuestionRuleService, CandidateQuestionRuleService>();

        return services;
    }
}
