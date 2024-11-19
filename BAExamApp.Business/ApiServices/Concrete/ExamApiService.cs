using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.Dtos.ApiDtos.ExamDtos;
using BAExamApp.Dtos.ApiDtos.StudentExamApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.ApiServices.Concrete;
public class ExamApiService : IExamApiService
{
    private readonly IExamRepository _examRepository;

    public ExamApiService(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    /// <summary>
    /// Öğrencinin adını, soyadıni, mailini, sınıfını, girdiği sınavın adını, skorunu ve kuralını döndürür.
    /// </summary>
    /// <returns></returns>
    public async Task<IDataResult<List<GetAllDataWithRegisterCodeDto>>> GetAllDataWithRegisterCodeAsync()
    {
        var values = (await _examRepository.GetAllAsync()).Select(x => new GetAllDataWithRegisterCodeDto
        {
            ExamName = x.Name,
            ExamRule = x.ExamRule?.Name,
            ExamClassroom = x.ExamClassrooms?.FirstOrDefault(y => y.ExamId == x.Id)?.Classroom?.Name,
            StudentInfo = x.StudentExams.Select(se => new StudentInfoAndScoreDto
            {
                StudentFirstName = se.Student?.FirstName,
                StudentLastName = se.Student?.LastName,
                StudentEmail = se.Student?.Email,
                ExamScore = se.Score
            }).ToList()
        }).ToList();

        if (values is not null) return new SuccessDataResult<List<GetAllDataWithRegisterCodeDto>>(values, Messages.FoundSuccess);
        return new ErrorDataResult<List<GetAllDataWithRegisterCodeDto>>(values, Messages.ListNotFound);
    }
}
