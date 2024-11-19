using BAExamApp.Dtos.Candidate.CandidateExamInitiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateExamInitiationService
{
    /// <summary>
    /// Sınav açılma ekranındaki bilgilerin getirilmesi için gerekli method
    /// </summary>
    /// <param name="candidateId"></param>
    /// <param name="examId"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateStartExamDetailsDto>> GetExamStartDetailsAsync(Guid candidateId, Guid examId);

    /// <summary>
    /// Aday sınav ekranında Sınavı başlattığında çalışan method
    /// </summary>
    /// <param name="candidateId"></param>
    /// <param name="examId"></param>
    /// <returns></returns>
    Task<IResult> StartExamAsync(Guid candidateId, Guid examId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="questionInOrderDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateExamGetQuestionInOrderDto>> GetQuestionInOrderAsync(CandidateExamGetQuestionInOrderDto questionInOrderDto);

    /// <summary>
    /// Verilen bilgilere uyan adayın sorusuna ait olan cevabı günceller.
    /// </summary>
    /// <param name="answerQuestionDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateExamAnswerQuestionDto>> AnswerQuestionAsync(CandidateExamAnswerQuestionDto answerQuestionDto);

    /// <summary>
    /// Verilen aday ve sınav ID bilgilerine uyan sınavı bitmiş duruma alır.
    /// </summary>
    /// <param name="candidateId"></param>
    /// <param name="examId"></param>
    /// <returns></returns>
    Task<IResult> FinishExamAsync(Guid candidateId, Guid examId);


}
