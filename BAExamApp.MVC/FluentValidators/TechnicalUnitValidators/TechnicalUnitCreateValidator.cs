using BAExamApp.MVC.Areas.Admin.Models.TechnicalUnitVMs;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.TechnicalUnitValidators;

public class TechnicalUnitCreateValidator : AbstractValidator<AdminTechnicalUnitCreateVM>
{
    public TechnicalUnitCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan boş bırakılamaz")
                           .MinimumLength(2).WithMessage("Teknik birim adı en az 2 karakterden oluşmalıdır")
                           .MaximumLength(500).WithMessage("Teknik birim adı en fazla {MaxLength} karakterden oluşmalıdır");
    }
}