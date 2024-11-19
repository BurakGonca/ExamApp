using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.Configurations.Candidate;
public class CandidateQuestionSubjectConfiguration : AuditableEntityConfiguration<CandidateQuestionSubject>
{
    public override void Configure(EntityTypeBuilder<CandidateQuestionSubject> builder)
    {
        base.Configure(builder);

        builder.ToTable("CandidateQuestionsSubject", "candidate");

        builder.Property(x => x.Name).IsRequired();
    }
}
