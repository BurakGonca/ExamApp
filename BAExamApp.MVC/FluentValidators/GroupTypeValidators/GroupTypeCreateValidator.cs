using BAExamApp.MVC.Areas.Admin.Models.GroupTypeVMs;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.GroupTypeValidators;

public class GroupTypeCreateValidator : AbstractValidator<AdminGroupTypeCreateVM>
{
    public GroupTypeCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Eğitim tipi boş bırakılamaz")
            .MinimumLength(2).WithMessage("Eğitim Tipi en az 2 karakterden oluşmalıdır")
            .Matches(@"^(?!'+$)[a-zA-Z'ğüşöçİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçİĞÜŞÖÇ]+)*$").WithMessage("Eğitim Tipi özel karakter ya da sayı içeremez");
        RuleFor(x => x.Information).NotEmpty().WithMessage("Açıklama boş bırakılamaz")
           .MaximumLength(500).WithMessage("Açıklama 500 karakterden uzun olamaz");
    }
}