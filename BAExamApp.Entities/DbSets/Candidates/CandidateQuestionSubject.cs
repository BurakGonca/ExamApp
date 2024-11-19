using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.DbSets.Candidates;
public class CandidateQuestionSubject : AuditableEntity
{
    public CandidateQuestionSubject()
    {
        //CandidateQuestions = new HashSet<CandidateQuestion>();
    }
    public string Name { get; set; }

    //Navigation Prop.

    public virtual ICollection<CandidateQuestion> CandidateQuestions { get; set; }
}
