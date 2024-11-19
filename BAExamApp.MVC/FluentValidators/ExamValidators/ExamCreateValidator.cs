using BAExamApp.Dtos.Exams;
using BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;
using FluentValidation;

namespace BAExamApp.MVC.FluentValidators.ExamValidators;

public class ExamCreateValidator : AbstractValidator<TrainerExamCreateVM>
{
    public ExamCreateValidator()
    {
        //RuleFor(x => x.ExamDateTime).NotEmpty().WithMessage("Bu alan boş bırakılamaz")
        //  .GreaterThan(DateTime.Today.AddHours(-1)).WithMessage("Geçmiş bir tarih seçemezsiniz");

        RuleFor(x => x.ExamDuration).NotEmpty().WithMessage("Bu alan boş bırakılamaz");

        RuleFor(x => x.ExamType).NotEmpty().WithMessage("Bu alan boş bırakılamaz");

        RuleFor(x => x.ExamCreationType).NotEmpty().WithMessage("Bu alan boş bırakılamaz");

        RuleFor(x => x.MaxScore).GreaterThan(0).WithMessage("0'dan büyük bir değer giriniz.");

        RuleFor(x => x.BonusScore).GreaterThanOrEqualTo(0).WithMessage("Lütfen bir doğal sayı değeri giriniz.");

        RuleFor(x => x.ExamRuleId).NotEmpty().WithMessage("Bu alan boş bırakılamaz. Eğer uygun bir sınav kuralı yoksa önce sınav kuralı ekleyiniz.");

        RuleFor(x => x.ExamClassroomsIds).NotEmpty().WithMessage("Lütfen bir sınıf seçiniz");

        RuleFor(x => x.ExamDateTime).GreaterThanOrEqualTo(DateTime.Now).WithMessage("Geçmiş bir tarihli sınav oluşturulamaz.");

        RuleFor(vm => vm.Description)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().When(vm => !vm.forClassroom)
            .WithMessage("Öğrenci için oluşturulan sınavlarda lütfen açıklama ekleyiniz.");
    }
}