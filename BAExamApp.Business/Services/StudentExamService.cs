using BAExamApp.Business.Interfaces.Services;
using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.Dtos.ApiDtos.StudentExamApiDtos;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.SendMails;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace BAExamApp.Business.Services;
public class StudentExamService : IStudentExamService
{
    private readonly IStudentExamRepository _studentExamRepository;
    private readonly ISendMailService _sendMailService;
    private IMapper _mapper;
    private readonly IStudentClassroomService _studentClassroomService;
    private readonly IClassroomService _classroomService;
    private readonly ITrainerClassroomService _trainerClassroomService;
    private readonly IExamAnalysisService _examAnalysisService;
    private readonly IStudentRepository _studentRepository;
    private readonly IExamRepository _examRepository;
    private readonly IStudentClassroomRepository _studentClassroomRepository;
    public StudentExamService(IStudentExamRepository studentExamRepository, ISendMailService sendMailService, IMapper mapper, IStudentClassroomService studentClassroomService, IClassroomService classroomService, ITrainerClassroomService trainerClassroomService, IExamAnalysisService examAnalysisService, ITrainerClassroomRepository trainerClassroomRepository, IStudentRepository studentRepository, IExamRepository examRepository, IStudentClassroomRepository studentClassroomRepository)
    {
        _studentExamRepository = studentExamRepository;
        _sendMailService = sendMailService;
        _mapper = mapper;
        _studentClassroomService = studentClassroomService;
        _classroomService = classroomService;
        _trainerClassroomService = trainerClassroomService;
        _examAnalysisService = examAnalysisService;
        _studentRepository = studentRepository;
        _examRepository = examRepository;
        _studentRepository = studentRepository;
    }

    public async Task<IDataResult<StudentExamDto>> GetByIdAsync(Guid id)
    {
        var studentExam = await _studentExamRepository.GetByIdAsync(id);
        if (studentExam == null)
        {
            return new ErrorDataResult<StudentExamDto>(Messages.StudentExamNotFound);
        }

        return new SuccessDataResult<StudentExamDto>(_mapper.Map<StudentExamDto>(studentExam), Messages.FoundSuccess);
    }

    public async Task<IDataResult<List<StudentExamListDto>>> GetAllByStudentIdAsync(Guid id)
    {
        var studentExams = await _studentExamRepository.GetAllAsync(x => x.StudentId == id);

        return new SuccessDataResult<List<StudentExamListDto>>(_mapper.Map<List<StudentExamListDto>>(studentExams), Messages.FoundSuccess);
    }

    public async Task<IDataResult<List<StudentExamListDto>>> GetAllByExamIdAsync(Guid id)
    {
        var studentExams = await _studentExamRepository.GetAllAsync(x => x.ExamId == id);

        return new SuccessDataResult<List<StudentExamListDto>>(_mapper.Map<List<StudentExamListDto>>(studentExams), Messages.FoundSuccess);
    }

    public async Task<IDataResult<StudentExamDto>> UpdateAsync(StudentExamUpdateDto studentExamUpdateDto)
    {
        var studentExam = await _studentExamRepository.GetByIdAsync(studentExamUpdateDto.Id);

        if (studentExam is null)
        {
            return new ErrorDataResult<StudentExamDto>(Messages.StudentExamNotFound);
        }

        var updatedStudentExam = _mapper.Map(studentExamUpdateDto, studentExam);

        await _studentExamRepository.UpdateAsync(updatedStudentExam);
        await _studentExamRepository.SaveChangesAsync();

        return new SuccessDataResult<StudentExamDto>(_mapper.Map<StudentExamDto>(updatedStudentExam), Messages.UpdateSuccess);
    }
    public async Task<IDataResult<List<StudentExamsDetailsDto>>> GetAllExamsWithDetailsByIdForTrainerAsync(Guid id)
    {
        var studentExams = await _studentExamRepository.GetAllAsync(x => x.StudentId == id);
        var studentsExamScores = studentExams.Where(x => x.Score != null);
        if (studentsExamScores.Count() == 0)
        {
            return new ErrorDataResult<List<StudentExamsDetailsDto>>(Messages.StudentExamsNotFound);
        }

        var studentsLatestClassroom = await _studentClassroomService.GetLatestClassroomByStudentIdForAdminAsync(id);

        if (studentsLatestClassroom.Data == null)
        {
            return new ErrorDataResult<List<StudentExamsDetailsDto>>(Messages.ClassroomNotFound);
        }

        var studentsLatestClassroomId = studentsLatestClassroom.Data.ClassroomId;
        var studentsLatestClassroomname = await _classroomService.GetByIdAsync(studentsLatestClassroomId);
        var studentsLatestClassroomsTrainers = await _trainerClassroomService.GetTrainersWithSpesificClassroomIdAsync(studentsLatestClassroomId);

        var studentExamsDetails = _mapper.Map<List<StudentExamsDetailsDto>>(studentsExamScores);
        foreach (var item in studentExamsDetails)
        {
            item.LatestClassroom = studentsLatestClassroomname.Data.Name;
            item.LatestClassroomsTrainers = studentsLatestClassroomsTrainers.Data != null && studentsLatestClassroomsTrainers.Data.Any() ? string.Join(", ", studentsLatestClassroomsTrainers.Data.Select(x => $"{x.FirstName} {x.LastName}")) : "";
        }

        return new SuccessDataResult<List<StudentExamsDetailsDto>>(studentExamsDetails, Messages.FoundSuccess);
    }


    public async Task<IDataResult<List<StudentExamsDetailsDto>>> GetAllExamsWithDetailsByIdAsync(Guid id)
    {
        var studentExams = await _studentExamRepository.GetAllAsync(x => x.StudentId == id);
        if (studentExams.Count() == 0)
        {
            return new ErrorDataResult<List<StudentExamsDetailsDto>>(Messages.StudentExamsNotFound);
        }

        var studentsLatestClassroom = await _studentClassroomService.GetLatestClassroomByStudentIdForAdminAsync(id);

        if (studentsLatestClassroom.Data == null)
        {
            return new ErrorDataResult<List<StudentExamsDetailsDto>>(Messages.ClassroomNotFound);
        }

        var studentsLatestClassroomId = studentsLatestClassroom.Data.ClassroomId;
        var studentsLatestClassroomname = await _classroomService.GetByIdAsync(studentsLatestClassroomId);
        var studentsLatestClassroomsTrainers = await _trainerClassroomService.GetTrainersWithSpesificClassroomIdAsync(studentsLatestClassroomId);

        var studentExamsDetails = _mapper.Map<List<StudentExamsDetailsDto>>(studentExams);
        foreach (var item in studentExamsDetails)
        {
            item.LatestClassroom = studentsLatestClassroomname.Data.Name;
            item.LatestClassroomsTrainers = studentsLatestClassroomsTrainers.Data != null && studentsLatestClassroomsTrainers.Data.Any() ? string.Join(", ", studentsLatestClassroomsTrainers.Data.Select(x => $"{x.FirstName} {x.LastName}")) : "";
        }

        return new SuccessDataResult<List<StudentExamsDetailsDto>>(studentExamsDetails, Messages.FoundSuccess);
    }

    public async Task<IDataResult<List<StudentExamsAdminDto>>> GetAllExamsByStudentIdAsync(Guid id)
    {
        var studentExams = await _studentExamRepository.GetAllAsync(x => x.StudentId == id);

        if (studentExams.Count() == 0)
        {
            return new ErrorDataResult<List<StudentExamsAdminDto>>(Messages.StudentExamsNotFound);
        }

        return new SuccessDataResult<List<StudentExamsAdminDto>>(_mapper.Map<List<StudentExamsAdminDto>>(studentExams), Messages.FoundSuccess);
    }
    public async Task<IDataResult<ExamStrudentQuestionDetailsDto>> GetExamStrudentQuestionDetailsByIdAsync(Guid id)
    {
        var studentExam = await _studentExamRepository.GetByIdAsync(id);
        if (studentExam == null)
        {
            return new ErrorDataResult<ExamStrudentQuestionDetailsDto>(Messages.StudentExamNotFound);
        }

        var examStrudentQuestionDetails = _mapper.Map<ExamStrudentQuestionDetailsDto>(studentExam);

        return new SuccessDataResult<ExamStrudentQuestionDetailsDto>(examStrudentQuestionDetails, Messages.FoundSuccess);
    }

    public async Task<string> GetStudentEmailByStudentExamAsync(Guid studentExamId)
    {
        var email = await _studentExamRepository.GetStudentEmailByStudentExamAsync(studentExamId);
        return email;
    }

    public async Task<IDictionary<string, double>> AnalysisExamPerformanceByTrainerAsync(Guid trainerId, Guid examId)
    {

        var studentExams = await _studentExamRepository.GetStudentExamsByTrainerIdAsync(trainerId);

        var examSpecificStudentExams = studentExams.Where(se => se.ExamId == examId).ToList();

        var subtopicPerformances = new Dictionary<string, List<double>>();

        foreach (var studentExam in examSpecificStudentExams)
        {
            var subtopicPerformance = _examAnalysisService.CalculateOverallPerformance(studentExam);

            foreach (var subtopic in subtopicPerformance.Score.Keys)
            {
                if (!subtopicPerformances.ContainsKey(subtopic))
                {
                    subtopicPerformances[subtopic] = new List<double>();
                }
                subtopicPerformances[subtopic].Add(subtopicPerformance.Score[subtopic]);
            }
        }

        return subtopicPerformances.ToDictionary(
            k => k.Key,
            v => v.Value.Average());

    }
    public async Task<List<StudentExam>> GetStudentExamsByExamIdAsync(Guid examId)
    {
        var studentExamList = await _studentExamRepository.GetAllAsync();
        var filteredExamList = studentExamList.Where(s => s.ExamId == examId).ToList();

        return filteredExamList;
    }

    public async Task CheckAndSendExamResultsAsync()
    {
        var now = DateTime.Now;

        var unsentStudentExams = await _studentExamRepository.GetUnsendedStudentExamsAsync();

        var examsToProcess = unsentStudentExams
            .Select(se => se.Exam)
            .Distinct()
            .Where(exam => exam.ExamDateTime.Add(exam.ExamDuration) <= now)
            .ToList();

        foreach (var exam in examsToProcess)
        {
            var relevantStudentExams = unsentStudentExams
                .Where(se => se.ExamId == exam.Id)
                .ToList();

            var classroomResults = new Dictionary<Guid, List<AllStudentExamResultDto>>();

            foreach (var studentExam in relevantStudentExams)
            {
                var examInfo = studentExam.Exam;
                var studentInfo = studentExam.Student;
                var studentClassrooms = studentInfo.StudentClassrooms; 

                
                foreach (var examClassroom in examInfo.ExamClassrooms)
                {
                    var examClassroomId = examClassroom.ClassroomId; 

                    foreach (var studentClassroom in studentClassrooms)
                    {
                        if (studentClassroom.ClassroomId == examClassroomId)
                        {
                            if (!classroomResults.ContainsKey(examClassroomId))
                            {
                                classroomResults[examClassroomId] = new List<AllStudentExamResultDto>();
                            }

                            if (!classroomResults[examClassroomId].Any(dto => dto.StudentId == studentExam.Student.Id))
                            {
                                var performance = await _examAnalysisService.AnalysisStudentPerformanceAsync(studentExam.StudentId, studentExam.ExamId);

                                var resultDto = new AllStudentExamResultDto
                                {
                                    StudentId = studentExam.StudentId,
                                    StudentFullName = $"{studentExam.Student.FirstName} {studentExam.Student.LastName}",
                                    ExamName = examInfo.Name,
                                    Score = studentExam.Score
                                };

                                classroomResults[examClassroomId].Add(resultDto);
                            }
                        }
                    }
                }
            }

            foreach (var classroomId in classroomResults.Keys)
            {
                var results = classroomResults[classroomId];

                if (results.Count > 0)
                {
                    var allStudentsEmailDto = new AllStudentsEmailDto
                    {
                        Students = results
                    };

                    var strategy = await _studentExamRepository.CreateExecutionStrategy();
                    await strategy.ExecuteAsync(async () =>
                    {
                        using var transactionScope = await _studentExamRepository.BeginTransactionAsync().ConfigureAwait(false);
                        try
                        {
                            foreach (var studentExam in relevantStudentExams
                                .Where(se => se.Exam.ExamClassrooms.Any(ec => ec.ClassroomId == classroomId)))
                            {
                                studentExam.IsEmailSent = true;
                                await _studentExamRepository.UpdateAsync(studentExam);
                                await _studentExamRepository.SaveChangesAsync();
                            }
                            await _sendMailService.AllStudentEmail(allStudentsEmailDto);
                            transactionScope.Commit(); 
                        }
                        catch (Exception ex)
                        {
                            transactionScope.Rollback(); 
                        }
                    });
                }
            }
        }
    }


    /// <summary>
    /// Dictionary biçimlendirilir. Değerler string formatına çevrilir.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    private string FormatDictionary(Dictionary<string, int> dictionary)
    {
        return string.Join(" - ", dictionary.Select(kv => $"{kv.Value}"));
    }

    /// <summary>
    /// Sınava girmeyip mazeret bildiren öğrencilerin mazeretlerinin onayı için kullanılan metod.
    /// </summary>
    /// <param name="studentExamId"></param>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public async Task<IResult> ConfirmStudentExcuse(string studentExamId, string studentId)
    {
        var stundetExam = await _studentExamRepository.GetByIdAsync(Guid.Parse(studentExamId));
        try
        {
            if (stundetExam.RetakeExam != true)
            {
                stundetExam.RetakeExam = true;
                await _studentExamRepository.SaveChangesAsync();
                return new SuccessResult();
            }
            return new ErrorResult("Mazeret daha önce onaylanmış.");
        }
        catch (Exception)
        {
            return new ErrorResult();
            throw;
        }
    }
}