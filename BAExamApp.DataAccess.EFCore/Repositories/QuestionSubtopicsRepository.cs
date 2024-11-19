using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.EFCore.Repositories;
public class QuestionSubtopicsRepository : EFBaseRepository<QuestionSubtopics>, IQuestionSubtopicsRepository
{
    public QuestionSubtopicsRepository(BAExamAppDbContext context) : base(context) { }
}
