using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAExamApp.Entities.ApiEntities
{
    public class ApiUser : BaseUser
    {
        //nav. prop.
        // public virtual ICollection<RegisterCode> Codes { get; set; }

        public byte[]? NewImage { get; set; }
    }
}