using Microsoft.AspNetCore.Http;
namespace BAExamApp.Dtos.Students;

public class StudentCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Gender { get; set; }
    //public DateTime DateOfBirth { get; set; }
    public IFormFile? NewImage { get; set; }
    //public Guid CityId { get; set; }
    public List<Guid> ClassroomIds { get; set; } 

}