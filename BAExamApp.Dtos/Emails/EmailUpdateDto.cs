namespace BAExamApp.Dtos.Emails;

public class EmailUpdateDto
{
    public Guid Id { get; set; }
    public string EmailAddress { get; set; }
    public string IdentityId { get; set; }
}
