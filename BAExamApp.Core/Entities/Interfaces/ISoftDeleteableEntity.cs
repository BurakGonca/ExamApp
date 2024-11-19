namespace BAExamApp.Core.Entities.Interfaces;

public interface ISoftDeleteableEntity : ICreateableEntity, IUpdateableEntity, IEntity
{
    string? DeletedBy { get; set; }
    DateTime? DeletedDate { get; set; }
}
