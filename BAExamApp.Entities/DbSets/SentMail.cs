using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.DbSets;
public class SentMail : AuditableEntity
{
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsSuccess { get; set; }

}

