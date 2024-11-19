using BAExamApp.Dtos.Candidate.CandidateExamRule;
using BAExamApp.Dtos.Candidate.CandidateExams;
using BAExamApp.Dtos.Candidate.Candidates;
using BAExamApp.Dtos.SendMails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateExamService
{
    /// <summary>
    /// Sınavı id ile çağırma işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateExamDto>> GetByIdAsync(Guid id);

    /// <summary>
    /// Tüm sınavları çağırma işlemi
    /// </summary>
    /// <returns></returns>
    Task<IDataResult<List<CandidateExamListDto>>> GetAllAsync();

    /// <summary>
    /// Sınav ekleme ve verilen sorulardan rastgele soru atama işlemi.
    /// </summary>
    /// <param name="candidateExamCreateDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateExamDto>> AddAsync(CandidateExamCreateDto candidateExamCreateDto, List<CandidateQuestionListDto> questions);



    Task<IDataResult<CandidateExamDto>> CreateExamForLinkAsync(CandidateExamLinkCreateDto candidateExamCreateDto);


    /// <summary>
    /// Sınav silme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(Guid id);

    /// <summary>
    /// Sınava ait detayları getirme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateExamDetailsDto>> GetDetailsByIdAsync(Guid id);

    /// <summary>
    /// Adaya göre sınavdaki soruları getirme işlemi.
    /// </summary>
    /// <param name="candidateId" name="examId></param>
    /// <returns></returns>
    Task<IDataResult<CandidateExamQuestionsByCandidateDto>> GetExamQuestionsByCandidateIdAsync(Guid examId,Guid candidateId);
    /// <summary>
    /// Adaya ve sınava göre verilen cevapları getirme işlemi.
    /// </summary>
    /// <param name="candidateId" name="examId></param>
    /// <returns></returns>
    Task<IDataResult<List<CandidateCandidateAnswersDto>>> GetGivenAnswersAsync(Guid examId,Guid candidateId);

    Task<IDataResult<List<CandidateExamListDto>>> StartExamAndNotifyCandidates(Guid examId, string link);

    Task<IResult> NotifyCandidateAdminAboutExamCreationAsync(Guid examId, string link);

    Task<IResult> AddCandidateAnswersAsync(Guid questionId, string givenAnswer);

    Task<IResult> NotifyCandidateAdminAboutExamLinkCreationAsync(Guid examId, string baseUrl);

    Task AddQuestionsToExam(CandidateExamDto examId, List<CandidateQuestionListDto> questions, Guid candidatesExamsId, CandidateExamRuleDto examRule);

    Task<IDataResult<List<ExamFinishedNotifyToCandidateAdminDto>>> ExamFinishedNotifyToCandidateAdmin(Guid candidateId, Guid examId);
}
