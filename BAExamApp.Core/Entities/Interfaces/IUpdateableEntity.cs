namespace BAExamApp.Core.Entities.Interfaces;

public interface IUpdateableEntity : ICreateableEntity, IEntity
{
    string ModifiedBy { get; set; }
    DateTime ModifiedDate { get; set; }
}
