using BAExamApp.Core.Entities.Interfaces;

namespace BAExamApp.Core.Entities.Base;

public abstract class AuditableEntity : BaseEntity, ISoftDeleteableEntity
{
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
}
