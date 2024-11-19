namespace BAExamApp.Dtos.Admins;

public class AdminListDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string IdentityId { get; set; }
    public byte[]? NewImage { get; set; }
}