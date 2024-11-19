using BAExamApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.SentMails;
public class SentMailCreateDto
{
    public string Email { get; set; }
    public string Subject { get; set; }
    public string  Content { get; set; }
    public bool IsSuccess { get; set; }
}
