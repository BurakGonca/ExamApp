using Microsoft.AspNetCore.Http;

namespace BAExamApp.Dtos.Admins;

public class AdminCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    //public DateTime DateOfBirth { get; set; }
    public bool Gender { get; set; }
    public IFormFile? NewImage { get; set; }
    //public Guid CityId { get; set; }
    public string? IdentityId { get; set; }

}