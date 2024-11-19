using BAExamApp.MVC.Areas.Admin.Models.TrainerVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;
using BAExamApp.MVC.FluentValidators.CustomValidators;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BAExamApp.MVC.FluentValidators.TrainerValidators;

public class TrainerUpdateValidator : AbstractValidator<AdminTrainerUpdateVM>
{
    public TrainerUpdateValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim boş bırakılamaz")
             .MinimumLength(2).WithMessage("İsim en az 2 karakterden oluşmalıdır")
             .Matches(@"^(?!'+$)[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+)*$").WithMessage("İsim özel karakter ya da sayı içeremez");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyisim boş bırakılamaz")
            .MinimumLength(2).WithMessage("Soyisim en az 2 karakterden oluşmalıdır")
            .Matches(@"^(?!'+$)[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+)*$").WithMessage("Soyisim özel karakter ya da sayı içeremez");        

        //RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Doğum Tarihi boş bırakılamaz").Must(AgeValidate).WithMessage("Eğitmen 18 yaşından küçük ve ya 100 yaşından büyük olamaz");

        RuleForEach(x => x.OtherEmails).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
            .NotEqual(x => x.Email).WithMessage("E-Posta değerleri aynı olamaz.");

        //RuleFor(x => x.TechnicalUnitId).NotEmpty().WithMessage("Teknik birim boş bırakılamaz");

        RuleFor(x => x.NewImage).SetValidator(new ProfileImageValidator()).When(model => model.NewImage != null);

    }
    private bool AgeValidate(DateTime value)
    {
        if (value >= DateTime.Now) return false;
        int age = DateTime.Now.Year - value.Year;
        if (age < 18 || age > 100) return false;
        else return true;
    }

    //public TrainerUpdateValidator(IStringLocalizer<TrainerQuestionUpdateVM> stringLocalizer)
    //    : this()
    //{
    //    RuleFor(t0 => t0.ProductIds).NotNull().WithMessage(stringLocalizer["Please_Select_Product"]);
    //}

}