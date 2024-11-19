using BAExamApp.Core.Enums;
using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.Entities.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateExamEvaluationService : ICandidateExamEvaluationService
{
    private readonly ICandidateCandidateQuestionRepository _candidateCandidateQuestionRepository;

    public CandidateExamEvaluationService(ICandidateCandidateQuestionRepository candidateCandidateQuestionRepository)
    {
        _candidateCandidateQuestionRepository = candidateCandidateQuestionRepository;
    }

    /// <summary>
    /// Eğitmenin verdiği puana göre bir adayın sınavındaki algoritma sorusunu değerlendirir.
    /// </summary>
    /// <param name="candidateCandidateQuestionId">Adayın sınav sorusunun benzersiz kimliği.</param>
    /// <param name="givenScore">Eğitmen tarafından verilen puan.</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    public async Task<IDataResult<CandidateCandidateQuestion>> EvaluateCandidateExamAlgorithmQuestionByTrainerAsync(Guid candidateCandidateQuestionId, int givenScore)
    {
        try
        {
            var candidateQuestion = await _candidateCandidateQuestionRepository.GetByIdAsync(candidateCandidateQuestionId);

            if (candidateQuestion == null)
            {
                return new ErrorDataResult<CandidateCandidateQuestion>(Messages.QuestionNotFound);
            }
            candidateQuestion.Score = givenScore;

            await _candidateCandidateQuestionRepository.UpdateAsync(candidateQuestion);
            await _candidateCandidateQuestionRepository.SaveChangesAsync();

            return new SuccessDataResult<CandidateCandidateQuestion>(candidateQuestion, Messages.UpdateSuccess);
        }
        catch (Exception)
        {

            return new ErrorDataResult<CandidateCandidateQuestion>(Messages.UpdateFail);
        }

    }

    /// <summary>
    /// Adayın sınavındaki bir test sorusunu otomatik olarak değerlendirir.
    /// </summary>
    /// <param name="candidateCandidateQuestionId">Adayın sınav sorusunun benzersiz kimliği.</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    public async Task<IResult> EvaluateCandidateExamClassicQuestionAsync(Guid candidateCandidateQuestionId)
    {
        try
        {
            var candidateQuestion = await _candidateCandidateQuestionRepository.GetByIdAsync(candidateCandidateQuestionId);

            if (candidateQuestion == null)
            {
                return new ErrorResult(Messages.QuestionNotFound);
            }


            if (candidateQuestion.Question.QuestionAnswers.Any(x => x.Answer == candidateQuestion.CandidateAnswer!.Answer && x.Status!=Status.Deleted))
            {
                candidateQuestion.CandidateAnswer!.IsRightAnswer = true;
                candidateQuestion.Score = candidateQuestion.MaxScore;
            }
            else
            {
                candidateQuestion.CandidateAnswer!.IsRightAnswer = false;
                candidateQuestion.Score = 0;
            }
            await _candidateCandidateQuestionRepository.UpdateAsync(candidateQuestion);
            await _candidateCandidateQuestionRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new ErrorResult(Messages.UpdateFail);
        }
        return new SuccessResult(Messages.UpdateSuccess);
    }

    /// <summary>
    /// Adayın sınavındaki bir klasik soruyu değerlendirir.
    /// </summary>
    /// <param name="candidateCandidateQuestionId">Adayın sınav sorusunun benzersiz kimliği.</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    public async Task<IResult> EvaluateCandidateExamTestQuestionAsync(Guid candidateCandidateQuestionId)
    {
        try
        {
            var candidateQuestion = await _candidateCandidateQuestionRepository.GetByIdAsync(candidateCandidateQuestionId);

            if (candidateQuestion == null)
            {
                return new ErrorResult(Messages.QuestionNotFound);
            }
            if (candidateQuestion.CandidateAnswer!.CandidateAnswerId == candidateQuestion.Question.QuestionAnswers.FirstOrDefault(x => x.IsRightAnswer && x.Status!=Status.Deleted)!.Id)
            {
                candidateQuestion.CandidateAnswer.IsRightAnswer = true;
                candidateQuestion.Score = candidateQuestion.MaxScore;
            }
            else
            {
                candidateQuestion.CandidateAnswer.IsRightAnswer = false;
                candidateQuestion.Score = 0;
            }
            await _candidateCandidateQuestionRepository.UpdateAsync(candidateQuestion);
            await _candidateCandidateQuestionRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new ErrorResult(Messages.UpdateFail);
        }
        return new SuccessResult(Messages.UpdateSuccess);

    }

    /// <summary>
    /// Bir sınavdaki tüm test ve klasik soruları değerlendirir. İsteğe bağlı olarak belirli bir adayın sorularını değerlendirir.
    /// </summary>
    /// <param name="examId">Sınavın benzersiz kimliği.</param>
    /// <param name="candidateId">Adayın benzersiz kimliği (isteğe bağlı).</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    public async Task<IResult> EvaluateAllTestAndClassicQuestionsAsync(Guid examId, Guid? candidateId = null)
    {
        IEnumerable<CandidateCandidateQuestion>? questions;
        if (candidateId == null)
        {
            questions = await _candidateCandidateQuestionRepository.GetAllAsync(x => x.CandidatesExams.CandidateExamId == examId);
        }
        else
        {
            questions = await _candidateCandidateQuestionRepository.GetAllAsync(x => x.CandidatesExams.CandidateId == candidateId && x.CandidatesExams.CandidateExamId == examId);
        }
        if (questions is null)
        {
            return new ErrorResult(Messages.QuestionNotFound);
        }

        foreach (var question in questions.Where(x => x.CandidateAnswer != null))
        {

            if (question.Question.CandidateQuestionType == CandidateQuestionType.Test)
            {
                await EvaluateCandidateExamTestQuestionAsync(question.Id);
            }
            if (question.Question.CandidateQuestionType == CandidateQuestionType.Classic)
            {
                await EvaluateCandidateExamClassicQuestionAsync(question.Id);
            }
            if (question.Question.CandidateQuestionType == CandidateQuestionType.Algorithm)
            {
                await AIAssessmentAsync(question.Id);
            }
        }

        return new SuccessResult(Messages.UpdateSuccess);
    }

    /// <summary>
    /// Bir algoritma sorusuna verilen cevabın yapay zeka ile değerlendirilmesini sağlar. İsteğe bağlı olarak yapay zeka cevabı yerine verilen metni kullanır.
    /// </summary>
    /// <param name="candidateQuestionId">Adayın sorusunun benzersiz kimliği.</param>
    /// <param name="content">Yeni metin (isteğe bağlı).</param>
    /// <returns>Değerlendirme sonucunu içeren asenkron işlemi temsil eden bir görev.</returns>
    public async Task<IDataResult<CandidateCandidateQuestion>> AIAssessmentAsync(Guid candidateQuestionId, string? content = null)
    {
        var candidateQuestion = await _candidateCandidateQuestionRepository.GetByIdAsync(candidateQuestionId);

        if (candidateQuestion == null)
        {
            return new ErrorDataResult<CandidateCandidateQuestion>(Messages.QuestionNotFound);
        }

        if (candidateQuestion.CandidateAnswer == null)
        {
            return new ErrorDataResult<CandidateCandidateQuestion>(Messages.QuestionNotFound);
        }
        if (content == null)
        {
            candidateQuestion.CandidateAnswer.AIAssessment = await EvaluateWithAIAsync(candidateQuestion);
        }
        else
        {
            candidateQuestion.CandidateAnswer.AIAssessment = content;

        }

        await _candidateCandidateQuestionRepository.UpdateAsync(candidateQuestion);
        await _candidateCandidateQuestionRepository.SaveChangesAsync();

        return new SuccessDataResult<CandidateCandidateQuestion>(candidateQuestion,Messages.UpdateSuccess);

    }

    private async Task<string> EvaluateWithAIAsync(CandidateCandidateQuestion question)
    {


        string send = ExamAIConstants.AI_TEXT +
            $"\n{ExamAIConstants.QUESTION} : {question.Question.Content!.Replace("\"", "\\\"")}." +
            $"\n{ExamAIConstants.CORRECT_ANSWER} : {question.Question.QuestionAnswers.First().Answer.Replace("\"", "\\\"")}." +
            $"\n{ExamAIConstants.GIVEN_ANSWER} : {(question.CandidateAnswer?.Answer!=null? question.CandidateAnswer!.Answer!.Replace("\"", "\\\""):"No answer provided")}.";


        string jsonBody = $@"{{
                ""contents"": [
                    {{
                        ""role"": """",
                        ""parts"": [
                            {{
                                ""text"": ""{send}""
                            }}
                        ]
                    }}
                ],
                ""generationConfig"": {{
                    ""temperature"": 0.9,
                    ""topK"": 50,
                    ""topP"": 0.95,
                    ""maxOutputTokens"": 4096,
                    ""stopSequences"": []
                }},
                ""safetySettings"": [

                ]
            }}
        
			";

        using var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.0-pro:generateContent?key={ExamAIConstants.API_KEY}");
        request.Content = new StringContent(jsonBody, Encoding.UTF8);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await client.SendAsync(request).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);

            return json["candidates"][0]["content"]["parts"][0]["text"].ToString();

        }
        else
        {
            return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
        }
    }


    private static class ExamAIConstants
    {
        public const string API_KEY = "AIzaSyA31By3_Y8TriffeUFYxlElFzRneBdWGZs";
        public const string QUESTION = "Soru";
        public const string CORRECT_ANSWER = "Doğru Cevap";
        public const string GIVEN_ANSWER = "Katılımcı Cevabı";

        public const string AI_TEXT = "Bir algoritma sınavı yapıyorum. Bu sınavda sorulan sorunun sistemde kayıtlı bir doğru cevabı var. Katılımcının verdiği cevabın sorulan soru baz alınarak sistemdeki doğru cevabı ne kadar/hangi oranda karşıladığının sonuçlarını ver. 100 kelimeyi geçmesin.";

        public const string AI_TEXT_2 = "Bir algoritma sınavı yapıyorum. Bu sınavda sorulan sorunun sistemde kayıtlı bir doğru cevabı var. Katılımcının verdiği cevabın sorulan soru baz alınarak sistemdeki doğru cevabı ne kadar/hangi oranda karşıladığının sonuçlarını adım adım karşılaştırarak ver.";

    }
}
