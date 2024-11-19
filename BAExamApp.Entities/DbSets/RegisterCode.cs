using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.DbSets;
public class RegisterCode : AuditableEntity
{
    public string Code { get; set; }
    public DateTime CodeCreationTime{ get; set; } = DateTime.Now;
    public DateTime CodeExpirationTime{ get; set; } = DateTime.Now.AddMinutes(15);
    public string CreatedForId { get; set; }
}
