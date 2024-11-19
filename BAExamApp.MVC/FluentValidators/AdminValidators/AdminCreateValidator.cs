using BAExamApp.MVC.Areas.Admin.Models.AdminVMs;
using BAExamApp.MVC.FluentValidators.CustomValidators;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.AdminValidators;

public class AdminCreateValidator : AbstractValidator<AdminAdminCreateVM>
{
    public AdminCreateValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim boş bırakılamaz")
            .MinimumLength(2).WithMessage("İsim en az 2 karakterden oluşmalıdır")
            .Matches(@"^(?!'+$)[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+)*$").WithMessage("İsim özel karakter ya da sayı içeremez");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyisim boş bırakılamaz")
            .MinimumLength(2).WithMessage("Soyisim en az 2 karakterden oluşmalıdır")
            .Matches(@"^(?!'+$)[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+)*$").WithMessage("Soyisim özel karakter ya da sayı içeremez");

        RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta boş bırakılamaz")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");

        RuleFor(x => x.OtherEmails).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
            .NotEqual(x=>x.Email).WithMessage("E-Posta değerleri aynı olamaz.");


        //RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Doğum Tarihi boş bırakılamaz")
        //        .LessThanOrEqualTo(DateTime.Now).WithMessage("Doğum Tarihi ileri bir tarih olamaz");



        RuleFor(x => x.NewImage).SetValidator(new ProfileImageValidator()!).When(model => model.NewImage != null);
    }
}
