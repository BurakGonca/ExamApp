using BAExamApp.MVC.Areas.Admin.Models.ProductVMs;
using BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.SubjectValidators;

public class SubjectUpdateValidators : AbstractValidator<AdminSubjectUpdateVM>
{
    public SubjectUpdateValidators()
    {
        RuleFor(x => x.ProductIds).NotEmpty().WithMessage("Ürün seçilmeden konu eklenemez");
    }
}
