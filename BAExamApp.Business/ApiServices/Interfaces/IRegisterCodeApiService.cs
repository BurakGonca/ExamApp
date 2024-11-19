using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.ApiServices.Interfaces;
public interface IRegisterCodeApiService
{
    /// <summary>
    /// Üretilen kodun doğruluğunu kontrol eder.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<bool> IsRegisterCodeActiveAsync(string code);

    Task<IDataResult<string>> GenerateCodeOnLoginAsync(string id);

}
