using BAExamApp.Dtos.Students;

namespace BAExamApp.Business.Interfaces.Services;

public interface IStudentService
{
    Task<IDataResult<StudentDto>> GetByIdAsync(Guid id);
    Task<IDataResult<StudentDto>> GetByIdentityIdAsync(string identityId);
    Task<IDataResult<StudentDetailsForTrainerDto>> GetDetailsByIdForTrainerAsync(Guid id);
    Task<IDataResult<List<StudentListDto>>> GetAllAsync();
    Task<IDataResult<List<StudentListDto>>> GetAllByClassroomIdAsync(Guid id);
    Task<IDataResult<List<StudentListDto>>> GetStudentsWithoutSpesificClassroomIdAsync(Guid id);
    Task<IDataResult<List<StudentListDto>>> GetStudentsWithSpesificClassroomIdAsync(Guid id);
    /// <summary>
    /// Students tablosundaki silinmemiş tüm öğrencilerin listesini döndüren metot.
    /// </summary>
    /// <returns></returns>
    Task<IDataResult<List<StudentListDto>>> GetActiveStudentsAsync();
    /// <summary>
    /// Parametre olarak girilen ad, soyad veya mail adresine göre öğrenci listesindeki öğrencileri filtreleyen metot.
    /// </summary>
    /// <param name="name">Öğrenci adına karşılık gelen değişken</param>
    /// <param name="surname">Öğrenci soyadına karşılık gelen değişken</param>
    /// <param name="mailAdress">Öğrenci mail adresine karşılık gelen değişken</param>
    /// <returns></returns>
    Task<IDataResult<List<StudentListDto>>> GetStudentsByNameOrSurnameOrMailAdressAsync(string? name, string? surname, string? mailAdress);
    Task<IDataResult<StudentDetailsDto>> GetStudentDetailsByIdAsync(Guid id);
    Task<IDataResult<StudentDto>> AddAsync(StudentCreateDto studentCreateDto);
    Task<IDataResult<StudentDto>> UpdateAsync(StudentUpdateDto studentUpdateDto);
    Task<IResult> DeleteAsync(Guid id);
    Task<bool> IsGraduatedAsync(string identityId);

}