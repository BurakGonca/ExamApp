using BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.SubjectValidators;

public class SubjectCreateValidator : AbstractValidator<AdminSubjectCreateVM>
{
    public SubjectCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan boş bırakılamaz")
                                .MinimumLength(2).WithMessage("Konu adı en az 2 karakterden oluşmalıdır");

        RuleFor(x => x.ProductIds).NotNull().NotEmpty().WithMessage("Ürün seçilmeden konu eklenemez");
    }
}