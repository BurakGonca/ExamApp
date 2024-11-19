namespace BAExamApp.Dtos.TrainerClassrooms;

public class TrainerClassroomListForTrainerDetailsDto
{
    public Guid Id { get; set; }
    public string ClassroomName { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public int StudentCount { get; set; }
}
