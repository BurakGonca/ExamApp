using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.Configurations.Candidate;
internal class CandidateQuestionRuleConfiguration : AuditableEntityConfiguration<CandidateQuestionRule>
{
    public override void Configure(EntityTypeBuilder<CandidateQuestionRule> builder)
    {
        base.Configure(builder);

        builder.ToTable("CandidateQuestionRules", "candidate");

    }
}
