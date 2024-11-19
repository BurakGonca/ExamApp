using BAExamApp.Core.Enums;

namespace BAExamApp.Core.Entities.Interfaces;

public interface IEntity
{
    Guid Id { get; set; }
    Status Status { get; set; }
}
