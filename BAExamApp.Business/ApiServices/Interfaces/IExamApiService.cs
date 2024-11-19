using BAExamApp.Dtos.ApiDtos.ExamDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.ApiServices.Interfaces;
public interface IExamApiService
{
    /// <summary>
    /// Öğrencinin adını, soyadıni, mailini, sınıfını, girdiği sınavın adını, skorunu ve kuralını döndürür.
    /// </summary>
    /// <returns></returns>
    Task<IDataResult<List<GetAllDataWithRegisterCodeDto>>> GetAllDataWithRegisterCodeAsync();
}
