using BAExamApp.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.EFCore.Repositories;
public class StudentAnswerRepository : EFBaseRepository<StudentAnswer>, IStudentAnswerRepository
{
    public StudentAnswerRepository(BAExamAppDbContext context) : base(context) { }
}
