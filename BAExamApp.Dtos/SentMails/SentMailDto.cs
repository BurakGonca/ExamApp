using BAExamApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.SentMails;
public class SentMailDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public bool IsSuccess { get; set; }
    public string  Content { get; set; }  
    public Status Status { get; set; }

}
