using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidateCandidateAnswerConfiguration : BaseEntityConfiguration<CandidateCandidateAnswer>
{
    public override void Configure(EntityTypeBuilder<CandidateCandidateAnswer> builder)
    {
        base.Configure(builder);
        builder.ToTable("CandidateCandidateAnswers", "candidate");

        builder.HasOne(x => x.CandidateQuestion)
        .WithOne(x => x.CandidateAnswer)
        .HasForeignKey<CandidateCandidateQuestion>(x => x.CandidateAnswerId);

    }
}
