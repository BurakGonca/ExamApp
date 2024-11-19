using BAExamApp.Dtos.Classrooms;
using BAExamApp.Dtos.Emails;
using BAExamApp.Entities.DbSets;
using System.Linq.Expressions;

namespace BAExamApp.Business.Services;
public class EmailService : IEmailService
{
    private readonly IEmailRepository _emailRepository;
    private readonly IMapper _mapper;

    public EmailService(IEmailRepository emailRepository, IMapper mapper)
    {
        _emailRepository = emailRepository;
        _mapper = mapper;
    }

    public async Task<bool> AnyAsync(Expression<Func<Email, bool>> expression)
    {
        return await _emailRepository.AnyAsync(expression);
    }

    public async Task<IDataResult<List<EmailDto>>> GetAllByIdentityIdAsync(string identityId)
    {
        var emails = await _emailRepository.GetAllAsync(x => x.IdentityId == identityId);

        if (emails != null)
        {
            return new SuccessDataResult<List<EmailDto>>(_mapper.Map<List<EmailDto>>(emails), Messages.EmailFoundSuccess);
        }

        return new ErrorDataResult<List<EmailDto>>(Messages.EmailNotFound);
    }

    public async Task<IDataResult<EmailDto>> AddAsync(EmailCreateDto emailCreateDto)
    {
        var hasEmail = await _emailRepository.AnyAsync(x => x.EmailAddress.ToLower() == emailCreateDto.EmailAddress.ToLower());

        if (hasEmail)
        {
            return new ErrorDataResult<EmailDto>(Messages.AddFailAlreadyExists);
        }

        var email = _mapper.Map<Email>(emailCreateDto);

        await _emailRepository.AddAsync(email);
        await _emailRepository.SaveChangesAsync();

        return new SuccessDataResult<EmailDto>(_mapper.Map<EmailDto>(email), Messages.AddSuccess);
    }

    public async Task<IDataResult<List<EmailDto>>> AddRangeAsync(List<EmailCreateDto> emailsCreateDto)
    {
        var emails = new List<Email>();
        var uniqueEmailsCreateDtoList = emailsCreateDto
        .Where(x => !string.IsNullOrEmpty(x.EmailAddress))
        .GroupBy(x => x.EmailAddress)
        .Select(group => group.First())
        .ToList();

        foreach (var uniqueEmailCreateDto in uniqueEmailsCreateDtoList)
        {
            var email = _mapper.Map<Email>(uniqueEmailCreateDto);

            await _emailRepository.AddAsync(email);

            emails.Add(email);
        }
        await _emailRepository.SaveChangesAsync();

        return new SuccessDataResult<List<EmailDto>>(_mapper.Map<List<EmailDto>>(emails), Messages.AddSuccess);
    }

    public async Task<IDataResult<EmailDto>> UpdateAsync(EmailUpdateDto emailUpdateDto)
    {
        var email = await _emailRepository.GetByIdAsync(emailUpdateDto.Id);

        if (email is null)
        {
            return new ErrorDataResult<EmailDto>(Messages.EmailNotFound);
        }

        var updatedEmail = _mapper.Map(emailUpdateDto, email);

        await _emailRepository.UpdateAsync(updatedEmail);
        await _emailRepository.SaveChangesAsync();

        return new SuccessDataResult<EmailDto>(_mapper.Map<EmailDto>(updatedEmail), Messages.UpdateSuccess);
    }

    public async Task<IDataResult<List<EmailDto>>> UpdateRangeAsync(List<EmailCreateDto> emailsCreateDto, string identityId)
    {

        var currentEmails = await _emailRepository.GetAllAsync(x => x.IdentityId == identityId);
        await DeleteRangeAsync(currentEmails.Select(x => x.Id).ToList());


        return await AddRangeAsync(emailsCreateDto);
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var email = await _emailRepository.GetByIdAsync(id);

        if (email is null)
        {
            return new ErrorDataResult<EmailDto>(Messages.EmailNotFound);
        }

        await _emailRepository.DeleteAsync(email);
        await _emailRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IResult> DeleteRangeAsync(List<Guid> ids)
    {
        foreach (var id in ids)
        {
            var email = await _emailRepository.GetByIdAsync(id);

            if (email is null)
            {
                return new ErrorDataResult<EmailDto>(Messages.EmailNotFound);
            }

            await _emailRepository.DeleteAsync(email);
        }

        await _emailRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IDataResult<List<string>>> GetEmailAddressesByIdentityIdAsync(string identityId)
    {
        var emails = await _emailRepository.GetAllAsync(x => x.IdentityId == identityId);

        if (emails != null)
        {
            return new SuccessDataResult<List<string>>(emails.Select(x => x.EmailAddress).ToList(), Messages.EmailFoundSuccess);
        }

        return new ErrorDataResult<List<string>>(Messages.EmailNotFound);
    }


}
