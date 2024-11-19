using BAExamApp.Dtos.StudentClassrooms;

namespace BAExamApp.Business.Interfaces.Services;
public interface IStudentClassroomService
{

    Task<IDataResult<List<StudentClassroomDto>>> GetAllByStudentIdAsync(Guid id);
    Task<IDataResult<List<StudentClassroomListForStudentDto>>> GetAllByStudentIdForStudentAsync(Guid id);

    Task<IDataResult<List<string>>> GetStudetClassroomIdentityIdAsync(Guid studentId);
    /// <summary>
    /// Id si ile sorgusu yapilan ogrencinin son katildigi sinifi doner
    /// </summary>
    /// <param name="Guid id"></param>
    /// <returns>DataResult<StudentClassroomDto></returns>
    Task<IDataResult<StudentClassroomDto>> GetLatestClassroomByStudentIdForAdminAsync(Guid id);

    Task<IResult> AddStudentToClassroomAsync(StudentAddToClassroomDto studentAddToClassroom);

    Task<IDataResult<List<StudentClassroomDto>>> GetAllActiveStudentsInClassroomAsync(Guid classroomId);

    Task<IResult> DeleteStudentByClassroom(Guid id);
}
