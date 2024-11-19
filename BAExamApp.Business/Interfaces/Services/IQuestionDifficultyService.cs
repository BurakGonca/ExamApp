using BAExamApp.Dtos.QuestionDifficulty;

namespace BAExamApp.Business.Interfaces.Services;

public interface IQuestionDifficultyService
{
    /// <summary>
    /// Soru tiplerini listelemek için kullanılır.
    /// </summary>
    /// <returns></returns>
    Task<IDataResult<List<QuestionDifficultyListDto>>> GetAllAsync();
    /// <summary>
    /// Verilen Id ye göre Soru zorluğu tipi döner.
    /// </summary>
    /// <param name="id">Seçili soru tipinin id'si</param>
    /// <returns></returns>
    Task<IDataResult<QuestionDifficultyDto>> GetDetailsByIdAsync(Guid id);
    /// <summary>
    /// Soru tipi eklemek için kullanılır
    /// </summary>
    /// <param name="questionDifficultyCreateDto">Eklecenek olan model</param>
    /// <returns></returns>
    Task<IDataResult<QuestionDifficultyDto>> AddAsync(QuestionDifficultyCreateDto questionDifficultyCreateDto);
    /// <summary>
    /// Soru tipini değişitirmek için kullanılır
    /// </summary>
    /// <param name="questionDifficultyUpdateDto">Değiştirilecek olan model</param>
    /// <returns></returns>
    Task<IDataResult<QuestionDifficultyDto>> UpdateAsync(QuestionDifficultyUpdateDto questionDifficultyUpdateDto);
    /// <summary>
    /// Soru tipini silmek için kullanılır
    /// </summary>
    /// <param name="id">Seçili soru tipinin id'si</param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(Guid id);
}
