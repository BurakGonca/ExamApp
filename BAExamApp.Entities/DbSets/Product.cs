using BAExamApp.Core.Enums;

namespace BAExamApp.Entities.DbSets;

public class Product : AuditableEntity
{
    public Product()
    {
        ClassroomProducts = new HashSet<ClassroomProduct>();
        ProductSubjects = new HashSet<ProductSubject>();
        ExamRules = new HashSet<ExamRule>();
        TrainerProducts = new HashSet<TrainerProduct>();
    }

    public string Name { get; set; } = null!;
    public bool? IsActive { get; set; }

    //Navigation Prop.
    public Guid TechnicalUnitId { get; set; }
    public virtual TechnicalUnit? TechnicalUnit { get; set; }

    public virtual ICollection<ClassroomProduct> ClassroomProducts { get; set; }
    public virtual ICollection<ProductSubject> ProductSubjects { get; set; }
    public virtual ICollection<ExamRule> ExamRules { get; set; }
    public virtual ICollection<TrainerProduct> TrainerProducts { get; set; }
}