using BAExamApp.Dtos.TrainerClassrooms;
using System.Linq.Expressions;

namespace BAExamApp.Business.Interfaces.Services;
public interface ITrainerClassroomService
{
    /// <summary>
    /// Seçilen trainerların update edilmesini sağlar
    /// </summary>
    /// <param name="classroomId"></param>
    /// <param name="trainersIds"></param>
    /// <returns>ıResult</returns>
    Task<IResult> UpdateTrainersIdsInClassroom(Guid classroomId, List<Guid> trainersIds);
    /// <summary>
    /// Eğitmenleri verilen sorguya göre getirir
    /// </summary>
    /// <param name="classroomId"></param>
    /// <param name="trainersIds"></param>
    /// <returns>ıResult</returns>
    Task<IDataResult<List<TrainerClassromDto>>> GetAllByExpression(Expression<Func<TrainerClassroom,bool>> expression, bool tracking = true);

    /// <summary>
    /// ClassroomID'sine göre sınıf ismini getirir.
    /// </summary>
    /// <param name="id">İlgili sınıfın ismini bulmak için ClassroomId gereklidir.</param>
    /// <returns>string tipinde veri döner.</returns>
    Task<string> GetClassroomNameByClassroomId(Guid Id);

    /// <summary>
    /// Guid ClassroomId ye Göre Sınıfa Kayıtlı Eğitmen Var İse Eğitmenleri Getirir.
    /// </summary>
    /// <param name="classroomId">Guid classroomId</param>
    /// <returns>TrainerDto Tipinde Liste Döner</returns>
    Task<IDataResult<List<TrainerClassromDto>>> GetTrainersWithSpesificClassroomIdAsync(Guid classroomId);

    /// <summary>
    /// Eğitmenin TrainerId'sine göre sınıfları getirir.s
    /// </summary>
    /// <param name="id">İlgili eğitmenin sınıflarını bulmak için TrainerId gereklidir.</param>
    /// <returns>ClassroomExamListDto Tipinde Liste Döner</returns>
    Task<IDataResult<List<TrainerClassroomExamListDto>>> GetClassroomsExamsByTrainerId(Guid Id);

    Task<IResult> AddTrainersToClassroomAsync(TraninerAddClassroomDto classroomAddTrainerDto);
}
