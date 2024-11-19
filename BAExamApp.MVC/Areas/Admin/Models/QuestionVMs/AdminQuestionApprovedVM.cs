using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Entities.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;

public class AdminQuestionApprovedVM
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }
    [Display(Name = "Created_By")]
    public string CreatedBy { get; set; }
    [Display(Name = "Created_Date")]
    public DateTime CreatedDate { get; set; }
    [Display(Name = "Modified_Date")]
    public DateTime ModifiedDate { get; set; }

    [Display(Name = "Question_Content")]
    public string Content { get; set; }
    [Display(Name = "State")]
    public State State { get; set; }
    [Display(Name = "Question_Type")]
    public QuestionType QuestionType { get; set; }
    [Display(Name = "Image")]
    public string Image { get; set; }
    [Display(Name = "Average_Answer_Time")]
    public TimeSpan AverageAnswerTime { get; set; }
    [Display(Name = "Longest_Answer_Time")]
    public TimeSpan LongestAnswerTime { get; set; }
    [Display(Name = "Shortest_Answer_Time")]
    public TimeSpan ShortestAnswerTime { get; set; }

    [Display(Name = "RightAnswer_Count")]
    public int RightAnswerCount { get; set; }
    [Display(Name = "WrongAnswer_Count")]
    public int WrongAnswerCount { get; set; }
    [Display(Name = "EmptyAnswer_Count")]
    public int EmptyAnswerCount { get; set; }

    [Display(Name ="Times_Question_Used")]
    public int TimesQuestionUsedInExam { get; set; }
    [Display(Name = "Subject")]
    public string SubjectName { get; set; }


    [Display(Name = "Subtopic")]
    public List<string> SubtopicName { get; set; }
    [Display(Name = "Question_Difficulty")]
    public string QuestionDifficultyName { get; set; }
    [Display(Name = "Question_ReviewComment")]
    public string RequestDescription { get; set; }
    public string? RejectComment { get; set; }

    [Display(Name = "Trainer")]
    public Guid TrainerID { get; set; }
    public SelectList Trainers { get; set; }

    [Display(Name = "Question_Answers")]
    public List<QuestionAnswerDto> QuestionAnswers { get; set; }

    // RevisionConclusion,RequesterAdmin özelliği için ekran üzerinde görüntülenecek adı belirten bir DisplayAttribute.
    [Display(Name = "Revise_Comment")]
    public string? RevisionConclusion { get; set; }
    [Display(Name = "Requester_Admin")]
    public string? RequesterAdmin { get; set; }

    

    [Display(Name = "Question_ModifierName")]
    public string RequesterAdminName { get; set; } = null!;



    [Display(Name = "Question_ModifierFullName")]
    public string RequestedTrainerName { get; set; } = null!;

    public Guid QuestionId { get; set; }
}
