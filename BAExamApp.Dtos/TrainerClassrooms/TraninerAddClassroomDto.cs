namespace BAExamApp.Dtos.TrainerClassrooms;
public class TraninerAddClassroomDto
{
    public Guid ClassroomId { get; set; }
    public List<Guid> SelectedTrainersIds { get; set; }
}
