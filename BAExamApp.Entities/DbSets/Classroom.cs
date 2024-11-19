namespace BAExamApp.Entities.DbSets;

public class Classroom : AuditableEntity
{
    public Classroom()
    {
        StudentClassrooms = new HashSet<StudentClassroom>();
        ClassroomProducts = new HashSet<ClassroomProduct>();
        TrainerClassrooms = new HashSet<TrainerClassroom>();
        ExamClassrooms = new HashSet<ExamClassrooms>();
    }

    public string Name { get; set; } = null!;
    public DateTime OpeningDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public bool IsActive
    {
        get {
            if (DateTime.Now < ClosedDate)
            {
                return true;
            }
            else
                return false;
        }
    }

    //Navigation Prop.
    public Guid GroupTypeId { get; set; }
    public virtual GroupType? GroupType { get; set; }
    public Guid BranchId { get; set; }
    public virtual Branch? Branch { get; set; }

    public virtual ICollection<StudentClassroom> StudentClassrooms { get; set; }
    public virtual ICollection<ClassroomProduct> ClassroomProducts { get; set; }
    public virtual ICollection<TrainerClassroom> TrainerClassrooms { get; set; }
    public virtual ICollection<ExamClassrooms> ExamClassrooms { get; set;}
}