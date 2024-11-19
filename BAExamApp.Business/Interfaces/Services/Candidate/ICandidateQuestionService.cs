using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Dtos.Candidate.CandidateQuestions;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateQuestionService
{
    /// <summary>
    /// Retrieves a candidate question by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate question.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the candidate question DTO.</returns>
    Task<IDataResult<CandidateQuestionDto>> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves all candidate questions.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of candidate question DTOs.</returns>
    Task<IDataResult<List<CandidateQuestionListDto>>> GetAllAsync();

    /// <summary>
    /// Retrieves detailed information about a candidate question by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate question.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the detailed candidate question DTO.</returns>
    Task<IDataResult<CandidateQuestionDetailsDto>> GetDetailsByIdAsync(Guid id);

    /// <summary>
    /// Adds a new candidate question.
    /// </summary>
    /// <param name="candidateQuestionCreateDto">The DTO containing the data to create the candidate question.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created candidate question DTO.</returns>
    Task<IDataResult<CandidateQuestionDto>> AddAsync(CandidateQuestionCreateDto candidateQuestionCreateDto);

    /// <summary>
    /// Updates an existing candidate question.
    /// </summary>
    /// <param name="candidateQuestionUpdateDto">The DTO containing the updated data of the candidate question.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated candidate question DTO.</returns>
    Task<IDataResult<CandidateQuestionDto>> UpdateAsync(CandidateQuestionUpdateDto candidateQuestionUpdateDto);

    /// <summary>
    /// Deletes a candidate question by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate question.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result.</returns>
    Task<IResult> DeleteAsync(Guid id);

    /// <summary>
    /// Verilen parametlere göre CandidateQuestion verisinde filtreleme yaparak geriye değer döndürür.
    /// </summary>
    /// <param name="content">Aranacak sorunun içeriği</param>
    /// <param name="candidateQuestionType">Aranacak sorunun, soru tipi</param>
    /// <param name="candidateQuestionCreatedDate">Aranacak sorunun başlangıç tarihi</param>
    /// <returns>Geri dönüş tipi CandidateQuestionListDto modelidir.</returns>
    Task<IDataResult<List<CandidateQuestionListDto>>> GetQuestionBySearchValues(string? content, int? candidateQuestionType, Guid? candidateQuestionSubjectId);

    Task<IDataResult<List<CandidateQuestionListDto>>> GetActiveQuestionsAsync();

    Task<IDataResult<List<CandidateQuestionListDto>>> GetInactiveQuestionsAsync();

    Task<IDataResult<CandidateQuestionDto>> SetQuestionAndAnswersToActiveAsync(Guid id);

    Task<IDataResult<CandidateQuestionDto>> SetQuestionAndAnswersToInactiveAsync(Guid id);

    Task<IDataResult<CandidateQuestionDto>> GetByIdWithFilteredAnswersAsync(Guid id);

    Task<IDataResult<List<CandidateQuestionDto>>> GetQuestionsBySubjectIdAsync(Guid subjectId);

}
