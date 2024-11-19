using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.ApiServices.Interfaces;
public interface IStudentApiService
{
    /// <summary>
    /// Öğrencinin mail adresine göre öğrenciyi getirir.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<IDataResult<Student>> GetAStudentByEmail(string email);

}
