using BAExamApp.MVC.Areas.Admin.Models.BranchVMs;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.BranchValidators;

public class BranchCreateValidator : AbstractValidator<AdminBranchCreateVM>
{
    public BranchCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan boş bırakılamaz")
                            .MinimumLength(2).WithMessage("Şube adı en az 2 karakterden oluşmalıdır")
                            .MaximumLength(256).WithMessage("Şube adı en fazla {MaxLength} karakterden oluşmalıdır");
    }
}