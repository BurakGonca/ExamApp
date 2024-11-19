using BAExamApp.DataAccess.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.ApiServices.Concrete;
public class StudentApiService : IStudentApiService
{
    private readonly IStudentRepository _studentRepository;

    public StudentApiService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    /// <summary>
    /// Öğrencinin mail adresine göre öğrenciyi getirir.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<IDataResult<Student>> GetAStudentByEmail(string email)
    {
        var student = await _studentRepository.GetStudentByEmail(email);
        if (student != null)
        {
            return new SuccessDataResult<Student>(student, Messages.StudentFoundSuccess);
        }
        return new ErrorDataResult<Student>(student, Messages.StudentNotFound);
    }

}
