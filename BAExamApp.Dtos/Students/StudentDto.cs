using Microsoft.AspNetCore.Http;
namespace BAExamApp.Dtos.Students;

public class StudentDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Gender { get; set; }
    //public DateTime DateOfBirth { get; set; }
    public string? Image { get; set; }
    public byte[]? OriginalImage { get; set; } // Database'ye daha önce eklemiş olan resim
    public DateTime? GraduatedDate { get; set; }
    public string IdentityId { get; set; }
    public List<Guid> ClassroomIds { get; set; }
}