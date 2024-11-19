namespace BAExamApp.Dtos.Students;

public class StudentDetailsDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Gender { get; set; }
    //public DateTime DateOfBirth { get; set; }
    public byte[]? NewImage { get; set; }
    public DateTime? GraduatedDate { get; set; }
    public string IdentityId { get; set; }
}
