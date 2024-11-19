using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.Configurations.Candidate;
internal class CandidateExamRuleConfiguration : AuditableEntityConfiguration<CandidateExamRule>
{
    public override void Configure(EntityTypeBuilder<CandidateExamRule> builder)
    {
        base.Configure(builder);

        builder.ToTable("CandidateExamRules", "candidate");

    }
}
