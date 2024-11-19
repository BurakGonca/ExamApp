using BAExamApp.MVC.Areas.Trainer.Models.TrainerVMs;
using BAExamApp.MVC.FluentValidators.CustomValidators;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.TrainerValidators;

public class TrainerTrainerUpdateValidator : AbstractValidator<TrainerTrainerUpdateVM>
{
    public TrainerTrainerUpdateValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim boş bırakılamaz")
             .MinimumLength(2).WithMessage("İsim en az 2 karakterden oluşmalıdır")
             .Matches(@"^(?!'+$)[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+)*$").WithMessage("İsim özel karakter ya da sayı içeremez");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyisim boş bırakılamaz")
            .MinimumLength(2).WithMessage("Soyisim en az 2 karakterden oluşmalıdır")
            .Matches(@"^(?!'+$)[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+)*$").WithMessage("Soyisim özel karakter ya da sayı içeremez");

        //RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Doğum Tarihi boş bırakılamaz").Must(AgeValidate).WithMessage("Eğitmen 18 yaşından küçük ve ya 100 yaşından büyük olamaz");
        //RuleFor(x => x.ImageFile).SetValidator(new ProfileImageValidator()).When(model => model.ImageFile != null);

    }
    private bool AgeValidate(DateTime value)
    {
        if (value >= DateTime.Now) return false;
        int age = DateTime.Now.Year - value.Year;
        if (age < 18 || age > 100) return false;
        else return true;
    }

}
