using BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.ClassroomValidators;

public class ClassroomUpdateValidator : AbstractValidator<AdminClassroomUpdateVM>
{
    public ClassroomUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan boş bırakılamaz")
                            .MinimumLength(2).WithMessage("Sınıf adı en az 2 karakterden oluşmalıdır");
    }
}