using BAExamApp.Core.Entities.Interfaces;
using BAExamApp.Core.Enums;

namespace BAExamApp.Core.Entities.Base;

public abstract class BaseEntity : ICreateableEntity
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public DateTime ModifiedDate { get; set; }
}
