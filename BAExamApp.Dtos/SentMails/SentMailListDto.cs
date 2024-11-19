using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.SentMails;
public class SentMailListDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string StudentFullName { get; set; }
    public string LatestClassroomsTrainers { get; set; }
    public string LatestClassroom { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsSuccess { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

}
