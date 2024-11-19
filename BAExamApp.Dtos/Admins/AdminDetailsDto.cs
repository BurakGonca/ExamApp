namespace BAExamApp.Dtos.Admins;
public class AdminDetailsDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    //public DateTime DateOfBirth { get; set; }
    public bool Gender { get; set; }
    public byte[]? NewImage { get; set; }
    //public string CityName { get; set; }
    public string IdentityId { get; set; }
}
