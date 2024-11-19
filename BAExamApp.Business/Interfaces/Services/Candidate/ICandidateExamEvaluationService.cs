using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateExamEvaluationService
{
    /// <summary>
    /// Eğitmenin verdiği puana göre bir adayın sınavındaki algoritma sorusunu değerlendirir.
    /// </summary>
    /// <param name="candidateCandidateQuestionId">Adayın sınav sorusunun benzersiz kimliği.</param>
    /// <param name="givenScore">Eğitmen tarafından verilen puan.</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    Task<IDataResult<CandidateCandidateQuestion>> EvaluateCandidateExamAlgorithmQuestionByTrainerAsync(Guid candidateCandidateQuestionId,int givenScore);

    /// <summary>
    /// Adayın sınavındaki bir test sorusunu otomatik olarak değerlendirir.
    /// </summary>
    /// <param name="candidateCandidateQuestionId">Adayın sınav sorusunun benzersiz kimliği.</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    Task<IResult> EvaluateCandidateExamTestQuestionAsync(Guid candidateCandidateQuestionId);

    /// <summary>
    /// Adayın sınavındaki bir klasik soruyu değerlendirir.
    /// </summary>
    /// <param name="candidateCandidateQuestionId">Adayın sınav sorusunun benzersiz kimliği.</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    Task<IResult> EvaluateCandidateExamClassicQuestionAsync(Guid candidateCandidateQuestionId);

    /// <summary>
    /// Bir sınavdaki tüm test ve klasik soruları doğruluğunu kontrol eder ve algoritma sorularını yapay zeka ile değerlendirir. İsteğe bağlı olarak belirli bir adayın sorularını değerlendirir.
    /// </summary>
    /// <param name="examId">Sınavın benzersiz kimliği.</param>
    /// <param name="candidateId">Adayın benzersiz kimliği (isteğe bağlı).</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    Task<IResult> EvaluateAllTestAndClassicQuestionsAsync(Guid examId, Guid? candidateId = null);

    /// <summary>
    /// Bir algoritma sorusuna verilen cevabın yapay zeka ile değerlendirilmesini sağlar. İsteğe bağlı olarak yapay zeka cevabı yerine verilen metni kullanır.
    /// </summary>
    /// <param name="candidateQuestionId">Adayın sorusunun benzersiz kimliği.</param>
    /// <param name="content">Yeni metin (isteğe bağlı).</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    Task<IDataResult<CandidateCandidateQuestion>> AIAssessmentAsync(Guid candidateQuestionId, string? content = null);



}
