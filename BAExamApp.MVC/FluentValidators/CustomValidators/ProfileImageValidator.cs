using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.CustomValidators;

public class ProfileImageValidator : AbstractValidator<IFormFile>
{
    public ProfileImageValidator()
    {
        RuleFor(x => x.Length).LessThanOrEqualTo(5242880)
            .WithMessage("Dosya boyutu 5 MB'tan büyük olamaz");

        RuleFor(x => x.ContentType).Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
            .WithMessage("Sadece jpg/jpeg/png formatındaki dosyalar yüklenebilir");
    }
}