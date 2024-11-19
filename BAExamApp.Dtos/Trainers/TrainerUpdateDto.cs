using BAExamApp.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace BAExamApp.Dtos.Trainers;

public class TrainerUpdateDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool Gender { get; set; }
    public string Email {  get; set; }
    //public DateTime DateOfBirth { get; set; }
    public IFormFile? NewImage { get; set; }
    //public Guid CityId { get; set; }
    public List<Guid> ProductIds { get; set; }
    public Guid? TechnicalUnitId { get; set; }
    public bool RemoveImage { get; set; }
    public Status Status { get; set; }
}