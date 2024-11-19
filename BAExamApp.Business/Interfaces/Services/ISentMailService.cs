using BAExamApp.Dtos.SentMails;
using System.Linq.Expressions;

namespace BAExamApp.Business.Interfaces.Services;
public interface ISentMailService
{
    Task<IDataResult<SentMail>> GetByIdAsync(Guid id);
    Task<IDataResult<List<SentMailListDto>>> GetAllAsync();

    Task<IDataResult<List<SentMailListDto>>> GetAllByEmailAsync(string email);

    Task<IDataResult<List<SentMailListDto>>> GetAllSentMailWithDetailsByEmailAsync(string email);

    Task<IDataResult<SentMailDto>> AddAsync(SentMailCreateDto sentMailCreateDto);

    Task<IDataResult<SentMailUpdateDto>> UpdateAsync(SentMailUpdateDto sentMailUpdate);

    Task<bool> AnyAsync(Expression<Func<SentMail, bool>> expression);

    Task<IResult> DeleteAsync(Guid id);

    Task<IResult> DeleteRangeAsync(List<Guid> ids);
}
