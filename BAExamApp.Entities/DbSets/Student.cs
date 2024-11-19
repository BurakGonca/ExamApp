namespace BAExamApp.Entities.DbSets;

public class Student : BaseUser
{
    public Student()
    {
        StudentClassrooms = new HashSet<StudentClassroom>();
        StudentExams = new HashSet<StudentExam>();
    }

    public DateTime? GraduatedDate { get; set; }

    //Navigation Prop.
    //public Guid CityId { get; set; }
    //public virtual City? City { get; set; }

    public byte[]? NewImage { get; set; }
    public virtual ICollection<StudentClassroom> StudentClassrooms { get; set; }
    public virtual ICollection<StudentExam> StudentExams { get; set; }
}
