using BAExamApp.Core.Entities.Interfaces;
using BAExamApp.Dtos.Classrooms;
using System.Linq.Expressions;

namespace BAExamApp.Business.Interfaces.Services;

public interface IClassroomService
{
    Task<IDataResult<ClassroomDto>> GetByIdAsync(Guid id);

    Task<IDataResult<ClassroomDto>>  GetAsync(Expression<Func<Classroom, bool>> expression);

    /// <summary>
    /// Filtreyi doldurmak için tüm sınıfları isim ve id'sine göre getirir.
    /// </summary>
    /// <returns>Liste tipinde ClassroomFilterDto döndürür</returns>
    Task<IDataResult<List<ClassroomFilterDto>>> GetAllClassroomByFilter();

    Task<IDataResult<List<ClassroomListDto>>> GetAllAsync();
    Task<IDataResult<List<ClassroomListDto>>> GetAllByIdentityIdAsync(string id);
    Task<IDataResult<List<ClassroomListDto>>> GetActiveAsync();
    /// <summary>
    /// Eğer sınıf mevcutsa sınıf detaylarını döner.
    /// Sınıf detayları şunları içerir; Name, OpeningDate, ClosedDate, GroupTypeName, ProductName, Students ve Trainers
    /// </summary>
    /// <param name="id">İlgili sınıf ve detaylarını bulmak için ClassroomId gereklidir.</param>
    /// <returns>ClassroomDetailDto</returns>
    Task<IDataResult<ClassroomDetailsDto>> GetDetailsByIdAsync(Guid id);
    Task<IDataResult<ClassroomDetailsForAdminDto>> GetDetailsByIdForAdminAsync(Guid id);
    Task<IDataResult<ClassroomDto>> AddAsync(ClassroomCreateDto classroomCreateDto);
    Task<IDataResult<ClassroomDto>> UpdateAsync(ClassroomUpdateDto classroomUpdateDto);




    /// <summary>
    /// id'ye göre sınıfı bulan ilişkileri varsa diğer entity'leri status'u pasife çeken yoksa deleted'a 
    /// çeken metod
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(Guid id);

    /// <summary>
    /// Adminin sınıfları; sınıf ismine, şube ismine, eğitim türüne, açılış vve kapanış tarihine göre filtreleme yaparak getirmesini sağlayan metottur.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="branchName"></param>
    /// <param name="groupType"></param>
    /// <param name="openingDate"></param>
    /// <param name="closedDate"></param>
    /// <returns></returns>
    Task<IDataResult<List<ClassroomListDto>>> GetFilteredByNameOrBranchNameOrGroupTypeOrOpeningDateOrClosedDateAsync(string name, string branchName, string groupType, DateTime openingDate, DateTime closedDate);

    Task<bool> HasRelationship(Guid id);

    /// <summary>
    /// Sınıf kapanış tarihi tamamlanmamış, aktif olan sınıfları getiren metoddur.
    /// </summary>
    /// <returns>Aktif sınıfları temsil eden bir <see cref="ClassroomListDto"/> listesi döndürür.</returns>
    Task<IDataResult<List<ClassroomListDto>>> GetActiveClassroomsAsync();
    Task UpdateStudentStatusIfClassroomClosedAsync();
}
