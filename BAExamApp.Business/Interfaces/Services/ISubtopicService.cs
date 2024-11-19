using BAExamApp.Dtos.Subtopics;

namespace BAExamApp.Business.Interfaces.Services;
public interface ISubtopicService
{
    /// <summary>
    /// Bütün subtopicleri Getirir.
    /// </summary>
    /// <returns>Bütün Subtopicleri DataResult olarak döner.</returns>
    Task<IDataResult<List<SubtopicListDto>>> GetAllAsync();

    /// <summary>
    /// Subtopic için db ekleme işlemi yapar.
    /// </summary>
    /// <param name="subtopicCreateDto"></param>
    /// <returns>Data result döner.</returns>
    Task<IDataResult<SubtopicDto>> AddAsync(SubtopicCreateDto subtopicCreateDto);

    /// <summary>
    /// Verilen Id ye göre subtopicDetail döner.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>SubtopicDetailDto</returns>
    Task<IDataResult<SubtopicDetailDto>> GetDetailsByIdAsync(Guid id);

    /// <summary>
    /// Subtopic için db silme işlemi yapar. 
    /// </summary>
    /// <param name="subtopicId"></param>
    /// <returns>Result Döner</returns>
    Task<IResult> DeleteAsync(Guid subtopicId);

    /// <summary>
    /// Subject Id ye göre Subtopic getirir.
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns>Subject Id ye göre SubtopicListDto listesi döner.</returns>
    Task<IDataResult<List<SubtopicListDto>>> GetSubtopicBySubjectId(Guid subjectId);

    /// <summary>
    /// Verilen ProductId ye göre bütün Subtopicleri Getirir. 
    /// </summary>
    /// <param name="productId"></param>
    /// <returns>List<SubtopicListDto></returns>
    Task<IDataResult<List<SubtopicListDto>>> GetAllBySubtopicsAsync(Guid productId);

    /// <summary>
    /// Verilen Id ye göre SubtopicDto döner 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>SubtopicDto</returns>
    Task<IDataResult<SubtopicDto>> GetSubtopicById(Guid id);

    /// <summary>
    /// Subtopic üzerinde değişiklik yapıp db ye işler
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>SubtopicDto</returns>
    Task<IDataResult<SubtopicDto>> UpdateAsync(SubtopicUpdateDto entity);

  /// <summary>
  ///  Altkonunun sorusu olup olmadığını döner
  /// </summary>
  /// <param name="subtopicId"></param>
  /// <returns></returns>
    Task<bool> IsQuestionUsedSubtopicAsync(Guid subtopicId);
    /// <summary>
    ///Alt konunun sınav kuralı olup olmadığını döner
    /// </summary>
    /// <param name="subtopicId"></param>
    /// <returns></returns>
    Task<bool> IsRuleUsedSubtopicAsync(Guid subtopicId);
    Task<IResult> ChangeRuleStatusAsync(Guid subtopicId);
}
