using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Models;

public class NotificationVM
{
    public Guid Id { get; set; }

    [Display(Name = "Title")]
    public string Title { get; set; }

    [Display(Name = "Description")]
    public string Description { get; set; }

    [Display(Name = "Notification_Status")]
    public bool NotificationStatus { get; set; }
}