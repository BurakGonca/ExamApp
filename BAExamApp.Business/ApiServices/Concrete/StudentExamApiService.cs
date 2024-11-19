
using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.Business.ApiServices.Interfaces;
using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.Dtos.ApiDtos.StudentExamApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using static OfficeOpenXml.ExcelErrorValue;
using BAExamApp.DataAccess.Contexts;
namespace BAExamApp.Business.ApiServices.Concrete;

public class StudentExamApiService : IStudentExamApiService
{
    private IMapper _mapper;
    private readonly IStudentExamRepository _studentExamRepository;

    public StudentExamApiService(IStudentExamRepository studentExamRepository, IMapper mapper)
    {
        _studentExamRepository = studentExamRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Öğrencinin mailine göre sınav sonuçlarını getirir
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<IDataResult<List<StudentExam>>> GetStudentExamsResults(string email)
    {
        var studentExams = await _studentExamRepository.GetStudentExams(email);
        if (studentExams != null && studentExams.Count > 0)
        {
            return new SuccessDataResult<List<StudentExam>>(studentExams, Messages.FoundSuccess);
        }
        return new ErrorDataResult<List<StudentExam>>(Messages.StudentExamNotFound);
    }

    /// <summary>
    /// Sınıf id ye göre, öğrenci id,Öğrenci Adı ve skoru getirir.
    /// </summary>
    /// <param name="classroomId"></param>
    /// <returns></returns>
    public async Task<IDataResult<List<StudentExamListApiDto>>> GetStudentExamsByClassroomIdAsync(Guid classroomId)
    {
        var studentExams = await _studentExamRepository.GetStudentExamsByClassroomIdAsync(classroomId);

        if (studentExams == null)
        {
            return new ErrorDataResult<List<StudentExamListApiDto>>(Messages.StudentExamNotFound);
        }
        var StudentExamDto = _mapper.Map<List<StudentExamListApiDto>>(studentExams);

        return new SuccessDataResult<List<StudentExamListApiDto>>(StudentExamDto, Messages.FoundSuccess);
    }
}
