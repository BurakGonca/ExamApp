using BAExamApp.Business.ApiServices.Interfaces;
using MailKit.Search;
using MimeKit.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.ApiServices.Concrete;
public class RegisterCodeApiService : IRegisterCodeApiService
{
    private readonly IRegisterCodeRepository _registerCodeRepository;

    public RegisterCodeApiService(IRegisterCodeRepository registerCodeRepository)
    {
        _registerCodeRepository = registerCodeRepository;
    }

    /// <summary>
    /// Kullanıcının Id'sini kullanır ve 15 dk süreli kod üretir.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IDataResult<string>> GenerateCodeOnLoginAsync(string id)
    {
        var value = await _registerCodeRepository.GetAsync(x => x.CreatedForId == id && x.CodeExpirationTime > DateTime.Now);
        if (value == null)
        {
            RegisterCode newCode = new RegisterCode()
            {
                Code = (Guid.NewGuid().ToString()).Replace("-", ""),
                CreatedForId = id
            };
            await _registerCodeRepository.AddAsync(newCode);
            await _registerCodeRepository.SaveChangesAsync();

            return new SuccessDataResult<string>(newCode.Code, Messages.AddSuccess);
        }
        return new SuccessDataResult<string>(value.Code, Messages.FoundSuccess);
    }

    /// <summary>
    /// Üretilen kodun doğruluğunu kontrol eder.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<bool> IsRegisterCodeActiveAsync(string code)
    {
        var value = (await _registerCodeRepository.GetAsync(x => x.Code == code && x.CodeExpirationTime > DateTime.Now));
        if (value is null) return false;
        return true;
    }
}