using BAExamApp.MVC.Models;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.LoginValidators;

public class LoginValidator : AbstractValidator<LoginVM>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen mail adresinizi giriniz")
                             .EmailAddress().WithMessage("Lütfen mail adresinizi doğru formatta giriniz");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Lütfen şifrenizi giriniz");
    }
}