using BAExamApp.MVC.Areas.Admin.Models.StudentVMs;
using BAExamApp.MVC.FluentValidators.CustomValidators;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.StudentValidators;

public class StudentUpdateValidator : AbstractValidator<AdminStudentUpdateVM>
{
    public StudentUpdateValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Bu alan boş bırakılamaz.")
                            .MinimumLength(2).WithMessage("Öğrenci adı en az 2 karakterden oluşmalıdır.")
                            .Matches(@"^(?!'+$)[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+)*$").WithMessage("İsim özel karakter ya da sayı içeremez")
                            .MaximumLength(256).WithMessage("Öğrenci adı en fazla 256 karakterden oluşmalıdır.");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Bu alan boş bırakılamaz.")
                            .MinimumLength(2).WithMessage("Öğrenci soyadı en az 2 karakterden oluşmalıdır.")
                            .Matches(@"^(?!'+$)[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+(?:\s+[a-zA-Z'ğüşöçıİĞÜŞÖÇ]+)*$").WithMessage("Soyad özel karakter ya da sayı içeremez")
                            .MaximumLength(256).WithMessage("Öğrenci soyadı en fazla 256 karakterden oluşmalıdır.");

       // RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Bu alan boş bırakılamaz.")
       //.LessThan(x => DateTime.Today.AddYears(-15))
       //.WithMessage("Öğrenci 15 yaşından büyük olmalıdır.");

        RuleFor(x => x.NewImage).SetValidator(new ProfileImageValidator()).When(model => model.NewImage != null);
    }
}
