namespace BAExamApp.Dtos.QuestionArranges;

public class QuestionArrangeListDto
{
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid ArrangerAdminId { get; set; }
}
