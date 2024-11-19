using BAExamApp.Core.Enums;
using BAExamApp.Dtos.StudentClassrooms;
using BAExamApp.Entities.DbSets;
using System.Security.Claims;

namespace BAExamApp.Business.Services;
public class StudentClassroomService : IStudentClassroomService
{
    private readonly IStudentClassroomRepository _studentClassroomRepository;
    private readonly IMapper _mapper;


    public StudentClassroomService(IMapper mapper, IStudentClassroomRepository studentClassroomRepository)
    {
        _mapper = mapper;
        _studentClassroomRepository = studentClassroomRepository;
    }

    public async Task<IDataResult<List<StudentClassroomDto>>> GetAllByStudentIdAsync(Guid id)
    {
        var studentClassrooms = await _studentClassroomRepository.GetAllAsync(x => x.StudentId == id);

        return new SuccessDataResult<List<StudentClassroomDto>>(_mapper.Map<List<StudentClassroomDto>>(studentClassrooms), Messages.ListedSuccess);
    }

    /// <summary>
    /// Ogrencinin en son eklendigini sinifi ogrencinin id'sine gore getirir
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IDataResult<StudentClassroomDto>> GetLatestClassroomByStudentIdForAdminAsync(Guid id)
    {
        var studentClassrooms = await _studentClassroomRepository.GetAllAsync(x => x.StudentId == id);

        // studentClassrooms listesi boşsa hata döndür
        if (!studentClassrooms.Any())
        {
            return new ErrorDataResult<StudentClassroomDto>(Messages.ClassroomNotFound);
        }

        var latestStudentClassroom = studentClassrooms.OrderByDescending(x => x.ModifiedDate).First();

        return new SuccessDataResult<StudentClassroomDto>(_mapper.Map<StudentClassroomDto>(latestStudentClassroom), Messages.ListedSuccess);
    }


    public async Task<IDataResult<List<StudentClassroomListForStudentDto>>> GetAllByStudentIdForStudentAsync(Guid id)
    {
        var studentClassrooms = await _studentClassroomRepository.GetAllAsync(x => x.StudentId == id);

        return new SuccessDataResult<List<StudentClassroomListForStudentDto>>(_mapper.Map<List<StudentClassroomListForStudentDto>>(studentClassrooms), Messages.ListedSuccess);
    }



    public async Task<IDataResult<List<string>>> GetStudetClassroomIdentityIdAsync(Guid studentId)
    {
        var students = await _studentClassroomRepository.GetAllAsync(x => x.StudentId == studentId);

        if (students != null)
        {
            return new SuccessDataResult<List<string>>(students.Select(x => x.Classroom.Name).ToList(), Messages.EmailFoundSuccess);
        }

        return new ErrorDataResult<List<string>>(Messages.EmailNotFound);
    }

    public async Task<IResult> AddStudentToClassroomAsync(StudentAddToClassroomDto studentAddToClassroom)
    {
        var studentClassRoom = await _studentClassroomRepository.GetAllAsync(x => x.ClassroomId == studentAddToClassroom.ClassroomId);
        var allStudentClassroom = await _studentClassroomRepository.GetAllDataAsync();
        var toBeDeletedStudents = studentClassRoom.Where(x => !studentAddToClassroom.SelectedStudentIds.Contains(x.StudentId)).ToList();

        var newStudents = studentAddToClassroom.SelectedStudentIds
    .Where(studentId => !studentClassRoom.Any(tc => tc.StudentId == studentId))
    .Select(studentId => new StudentClassroom
    {
        ClassroomId = studentAddToClassroom.ClassroomId,
        StudentId = studentId
    })
    .ToList();

        if (toBeDeletedStudents != null)
        {
            foreach (var student in toBeDeletedStudents) //Sınıftan silinen öğrencinin statü durumunu deleted haline getirir.
            {
                student.Status = Status.Deleted;
                student.DeletedDate = DateTime.UtcNow;
            }
            await _studentClassroomRepository.DeleteRangeAsync(toBeDeletedStudents);
        }

        var updateStudentClassRoom = await _studentClassroomRepository.GetAllAsync(x => x.ClassroomId == studentAddToClassroom.ClassroomId); //Sınıfta güncellenen öğrenci var mı diye bakar.
        if (updateStudentClassRoom != null)
        {
            var updatedStudentClassRoom = allStudentClassroom.Where(prevStudent => newStudents.Any(newStudent => newStudent.StudentId == prevStudent.StudentId && newStudent.ClassroomId == prevStudent.ClassroomId)).ToList();

            if (updatedStudentClassRoom.Count > 0)
            {
                foreach (var item in updatedStudentClassRoom)
                {
                    item.Status = Status.Active;
                    item.DeletedBy = null;
                    item.DeletedDate = null;
                }
                newStudents = newStudents.Where(newStudent => !allStudentClassroom.Any(allStudent => allStudent.StudentId == newStudent.StudentId)).ToList();
            }

            await _studentClassroomRepository.AddRangeAsync(newStudents);
        }
        await _studentClassroomRepository.SaveChangesAsync();
        return new SuccessResult(Messages.UpdateSuccess);
    }

    /// <summary>
    /// Sınıf içerisinde statü durumları active olan öğrencileri getirir.
    /// </summary>
    /// <param name="classroomId"></param>
    /// <returns></returns>
    public async Task<IDataResult<List<StudentClassroomDto>>> GetAllActiveStudentsInClassroomAsync(Guid classroomId)
    {
        var activeStudentsInClassroom = await _studentClassroomRepository.GetActiveStudentsByClassroomIdAsync(classroomId);

        return new SuccessDataResult<List<StudentClassroomDto>>(_mapper.Map<List<StudentClassroomDto>>(activeStudentsInClassroom), Messages.ListedSuccess);
    }

    /// <summary>
    /// StudentClassroom tablosundan ögrenciyi silmektedir.
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public async Task<IResult> DeleteStudentByClassroom(Guid studentId)
    {
        var studentsClassrooms = await _studentClassroomRepository.GetAllAsync();
        var studentClassroomId = studentsClassrooms.FirstOrDefault(sc=>sc.StudentId == studentId).Id;
        var studentClassroom = await _studentClassroomRepository.GetByIdAsync(studentClassroomId);

        _studentClassroomRepository.DeleteAsync(studentClassroom);

        await _studentClassroomRepository.SaveChangesAsync();
        return new SuccessResult(Messages.DeleteSuccess);
    }
}
