
﻿using System;

﻿using BAExamApp.Dtos.ApiDtos.StudentExamApiDtos;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAExamApp.DataAccess.Interfaces.Repositories;

namespace BAExamApp.Business.ApiServices.Interfaces;
public interface IStudentExamApiService
{

    /// <summary>
    /// Öğrencinin mailine göre sınav sonuçlarını getirir
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<IDataResult<List<StudentExam>>> GetStudentExamsResults(string email);



    /// <summary>
    /// Sınıf id ye göre, öğrenci id,Öğrenci Adı ve skoru getirir.
    /// </summary>
    /// <param name="classroomId"></param>
    /// <returns></returns>
    Task<IDataResult<List<StudentExamListApiDto>>> GetStudentExamsByClassroomIdAsync(Guid classroomId);
}
