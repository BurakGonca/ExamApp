using BAExamApp.Dtos.ExamEvaluators;

namespace BAExamApp.Business.Interfaces.Services;

public interface IExamEvaluatorService
{
    Task<IDataResult<List<ExamEvaluatorListDto>>> GetAllByExamIdAsync(Guid id);
    Task<IDataResult<List<ExamEvaluatorListDto>>> GetAllByTrainerIdAsync(Guid id);
    Task<IDataResult<ExamEvaluatorDto>> AddAsync(ExamEvaluatorCreateDto examEvaluatorCreateDto);
    Task<IDataResult<List<ExamEvaluatorDto>>> AddRangeAsync(List<ExamEvaluatorCreateDto> examEvaluatorsCreateDto);
    Task<IResult> DeleteAsync(Guid id);
    Task<IResult> DeleteRangeAsync(List<Guid> ids);
    Task<IResult> UpdateExamEvaluatorsAsync(List<Guid> EvaluatorIds, Guid ExamId);
    Task<IDataResult<List<ExamEvaluatorListDto>>> GetExamsByFiltered(string Id, string? selectedClassroomId = null, string? selectedRulenameId = null, string? datetimePickerStart = null, string? datetimePickerEnd = null);
}
