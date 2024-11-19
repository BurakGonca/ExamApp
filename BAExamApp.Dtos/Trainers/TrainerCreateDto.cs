using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace BAExamApp.Dtos.Trainers;

public class TrainerCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Gender { get; set; }
    //public DateTime DateOfBirth { get; set; }
    public IFormFile? NewImage { get; set; }
    public Guid BranchId { get; set; }
    public Guid TechnicalUnitId { get; set; }
    public List<Guid> ProductIds { get; set; }
    public List<string>? OtherEmails { get; set; }
    public string? IdentityId { get; set; }
}