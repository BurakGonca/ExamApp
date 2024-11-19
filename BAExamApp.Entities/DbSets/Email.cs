namespace BAExamApp.Entities.DbSets;

public class Email : AuditableEntity
{
    public string EmailAddress { get; set; } = null!;

    //Navigation Prop.
    public string IdentityId{ get; set; }
}
