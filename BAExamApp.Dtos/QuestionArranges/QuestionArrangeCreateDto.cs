namespace BAExamApp.Dtos.QuestionArranges;

public class QuestionArrangeCreateDto
{
    public string Comment { get; set; }
    public Guid QuestionId { get; set; }
    public Guid ArrangerAdminId { get; set; }
}
