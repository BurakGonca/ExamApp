using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidateBranchConfiguration : AuditableEntityConfiguration<CandidateCandidateBranch>
{
    public override void Configure(EntityTypeBuilder<CandidateCandidateBranch> builder)
    {
        base.Configure(builder);
        builder.ToTable("CandidateBranches", "candidate");
    }
}
