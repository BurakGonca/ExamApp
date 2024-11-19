using BAExamApp.Dtos.ExamClassrooms;
using BAExamApp.Dtos.StudentClassrooms;
using BAExamApp.Dtos.TrainerClassrooms;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Trainer.Models.ClassroomVMs;

public class TrainerClassroomDetailsVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Classroom_Name")]
    public string Name { get; set; }
    [Display(Name = "Opening_Date")]
    public DateTime OpeningDate { get; set; }
    [Display(Name = "Closed_Date")]
    public DateTime ClosedDate { get; set; }
    [Display(Name = "Group_Type")]

    public string GroupTypeName { get; set; }
    [Display(Name = "Branch_Name")]
    public string BranchName { get; set; }

    [Display(Name = "Trainer_List")]
    public List<string> TrainerNames { get; set; }
    [Display(Name = "Product_List")]
    public List<string> ProductNames { get; set; }

    [Display(Name = "Student_List")]
    public List<StudentClassroomListForClassroomDetailsDto> StudentClassroomList { get; set; }

    [Display(Name = "Exam_List")]
    public List<ExamClassroomForClassroomDetailsDto> ExamClassroomList { get; set; }
}