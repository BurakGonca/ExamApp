using BAExamApp.MVC.Areas.Admin.Models.ProductVMs;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.EducationValidators;

public class ProductUpdateValidator : AbstractValidator<AdminProductUpdateVM>
{
    public ProductUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan boş bırakılamaz")
                            .MinimumLength(2).WithMessage("Ürün adı en az 2 karakterden oluşmalıdır")
                            .MaximumLength(256).WithMessage("Ürün adı en fazla {MaxLength} karakterden oluşmalıdır");

        RuleFor(x => x.TechnicalUnitId).NotNull().NotEmpty().WithMessage("Teknik birim seçmeden eğitim eklenemez.");
    }
}