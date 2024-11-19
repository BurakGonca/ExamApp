using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidateConfiguration : AuditableEntityConfiguration<CandidateCandidate>
{
    public override void Configure(EntityTypeBuilder<CandidateCandidate> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Image).IsRequired(false);
        builder.ToTable("Candidates", "candidate");
    }
}
