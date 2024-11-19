namespace BAExamApp.Dtos.TrainerClassrooms;
public class TrainerClassromDto
{

    public TrainerClassromDto()
    {
        OtherEmails = new List<string>();
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Image { get; set; }
    public Guid BranchId { get; set; }
    public Guid TechnicalUnitId { get; set; }
    public string IdentityId { get; set; }
    public List<Guid> ProductIds { get; set; }
    public List<string>? OtherEmails { get; set; }
    public List<Guid> TrainerTalentIds { get; set; }
}
