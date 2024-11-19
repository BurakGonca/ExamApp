using BAExamApp.Core.Enums;

namespace BAExamApp.Dtos.TrainerClassrooms;
public class TrainerClassroomListForClassroomDetailsDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime? ResignedDate { get; set; }
    public Status Status { get; set; }
}
