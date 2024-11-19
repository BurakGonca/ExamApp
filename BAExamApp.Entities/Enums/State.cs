using System.ComponentModel.DataAnnotations;

namespace BAExamApp.Entities.Enums;

public enum State
{
    [Display(Name = "Bekliyor")]
    Awaited = 1,
    [Display(Name = "Onaylandı")]
    Approved = 2,
    [Display(Name = "Test")]
    Test = 3,
    [Display(Name = "Reddedildi")]
    Rejected = 4,
    [Display(Name = "Gözden Geçirilmeli")]
    Reviewed = 6,
    [Display(Name = "Pasife Alındı")]
    Deactivated = 7
}