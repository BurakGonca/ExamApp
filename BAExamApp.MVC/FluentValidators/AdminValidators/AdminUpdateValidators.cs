using BAExamApp.MVC.Areas.Admin.Models.AdminVMs;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.AdminValidators;

public class AdminUpdateValidators
{
    public class AdminUpdateValidator : AbstractValidator<AdminAdminUpdateVM>
    {
        public AdminUpdateValidator()
        {
            //RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Doğum Tarihi boş bırakılamaz")
            //           .LessThanOrEqualTo(DateTime.Now).WithMessage("Doğum Tarihi ileri bir tarih olamaz");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş bırakılamaz.").EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        }


    }
}
