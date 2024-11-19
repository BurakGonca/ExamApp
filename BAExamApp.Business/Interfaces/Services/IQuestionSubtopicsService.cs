using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services;
public interface IQuestionSubtopicsService
{
    Task AddAsync(QuestionSubtopics questionSubtopics);
    Task UpdateAsync(QuestionSubtopics questionSubtopics);
    Task DeleteAsync(QuestionSubtopics questionSubtopics);
    Task<List<QuestionSubtopics>> GetAllAsync();
    Task<QuestionSubtopics> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
}
