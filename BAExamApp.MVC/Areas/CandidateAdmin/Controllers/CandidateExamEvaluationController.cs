using BAExamApp.Business.Services.Candidate;
using BAExamApp.DataAccess.EFCore.Repositories.Candidate;
using Microsoft.AspNetCore.Mvc;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class CandidateExamEvaluationController : CandidateAdminBaseController
{
    private readonly ICandidateExamEvaluationService _candidateExamEvaluationService;

    public CandidateExamEvaluationController(ICandidateExamEvaluationService candidateExamEvaluationService)
    {
        _candidateExamEvaluationService = candidateExamEvaluationService;
    }
    public async Task<IActionResult> AlgorithmQuestion(Guid id,int givenScore)
    {
        var result = await _candidateExamEvaluationService.EvaluateCandidateExamAlgorithmQuestionByTrainerAsync(id, givenScore);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }
        return RedirectToAction("CandidateExamDetails", controllerName: "CandidateExam", new {examId=result.Data.CandidatesExams.CandidateExamId,candidateId=result.Data.CandidatesExams.CandidateId});
    }
    public async Task<IActionResult> TestQuestion(Guid id)
    {
        var result = await _candidateExamEvaluationService.EvaluateCandidateExamTestQuestionAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }
        return RedirectToAction("CandidateExamDetails", controllerName: "CandidateExam");

    }
    public async Task<IActionResult> ClassicQuestion(Guid id)
    {
        var result = await _candidateExamEvaluationService.EvaluateCandidateExamClassicQuestionAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }
        return RedirectToAction("CandidateExamDetails", controllerName: "CandidateExam");

    }
    public async Task<IActionResult> CandidateExamQuestions(Guid examId , Guid candidateId)
    {
        var result = await _candidateExamEvaluationService.EvaluateAllTestAndClassicQuestionsAsync(examId,candidateId);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }
        return RedirectToAction("Details", controllerName: "CandidateExam", new {id=examId});

    }
    public async Task<IActionResult> ExamQuestions(Guid examId)
    {
        var result = await _candidateExamEvaluationService.EvaluateAllTestAndClassicQuestionsAsync(examId);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }
        return RedirectToAction("Index", controllerName: "CandidateExam");

    }
    public async Task<IActionResult> UpdateAIAssessment(Guid answerId,string content)
    {
        var result = await _candidateExamEvaluationService.AIAssessmentAsync(answerId, content);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }
        return RedirectToAction("CandidateExamDetails", controllerName: "CandidateExam", new { examId = result.Data.CandidatesExams.CandidateExamId, candidateId = result.Data.CandidatesExams.CandidateId });


    }
}
