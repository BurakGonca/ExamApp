using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidateGroupConfiguration : AuditableEntityConfiguration<CandidateCandidateGroup>
{
    public override void Configure(EntityTypeBuilder<CandidateCandidateGroup> builder)
    {
        base.Configure(builder);
        builder.ToTable("CandidateGroups", "candidate");

        base.Configure(builder);
        builder.HasOne(x => x.CandidateBranch).WithMany(x => x.CandidateGroups).HasForeignKey(x => x.CandidateBranchId);
    }
}
