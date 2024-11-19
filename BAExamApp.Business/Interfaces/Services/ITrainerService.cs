using BAExamApp.Dtos.Classrooms;
using BAExamApp.Dtos.Trainers;

namespace BAExamApp.Business.Interfaces.Services;

public interface ITrainerService
{
    Task<IDataResult<List<TrainerListDto>>> GetAllAsync();
    Task<IDataResult<List<TrainerListDto>>> GetAllActiveAsync();
    Task<IDataResult<TrainerDto>> AddAsync(TrainerCreateDto entity);
    Task<IDataResult<TrainerDto>> RoleAddAsync(TrainerCreateDto entity);

    Task<IDataResult<TrainerDto>> GetByIdAsync(Guid id);
    Task<IDataResult<TrainerDto>> GetByIdentityIdAsync(string identityId);
    /// <summary>
    /// Eğer eğitmen mevcutsa eğitmen detaylarını döner.
    /// Eğitmen detayları şunları içerir; FirstName, LastName, Email, DateOfBirth, Gender, Image, Classrooms
    /// </summary>
    /// <param name="id">İlgili sınıf ve detaylarını bulmak için TrainerId gereklidir.</param>
    /// <returns>DataResult<TrainerDetailsDto></returns>
    Task<IDataResult<TrainerDetailsDto>> GetTrainerDetailsByIdAsync(Guid id);
    Task<IDataResult<TrainerDetailsForTrainerDto>> GetDetailsByIdentityIdForTrainerAsync(string id);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<TrainerDto>> UpdateAsync(TrainerUpdateDto trainerUpdateDto);
    Task<IDataResult<List<ClassroomListDto>>> GetClassroomsByIdentityId(string identityId);
    /// <summary>
    /// Guid ProductId ye Göre Sınıfa Kayıtlı Eğitmen Var İse Eğitmenleri Getirir.
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<IDataResult<List<TrainerDto>>> GetTrainersWithSpesificProductIdAsync(Guid productId);
    Task<IDataResult<List<TrainerListDto>>> GetAllWithClassroomCountsAsync();




}
