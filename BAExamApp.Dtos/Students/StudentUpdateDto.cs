using Microsoft.AspNetCore.Http;
namespace BAExamApp.Dtos.Students;

public class StudentUpdateDto
{
    
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Gender { get; set; }
    //public DateTime DateOfBirth { get; set; }
    public IFormFile? NewImage { get; set; }
    public bool RemoveImage { get; set; }
    public DateTime? GraduatedDate { get; set; }
    public List<Guid> ClassroomIds { get; set; }
}