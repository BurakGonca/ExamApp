using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidateAnswerConfiguration : AuditableEntityConfiguration<CandidateAnswer>
{
    public override void Configure(EntityTypeBuilder<CandidateAnswer> builder)
    {
        base.Configure(builder);

        builder.ToTable("CandidateAnswers", "candidate");

        builder.Property(x => x.Answer).IsRequired().HasColumnType("nvarchar(max)");

        builder.HasOne(aq => aq.Question).WithMany(q => q.QuestionAnswers).HasForeignKey(q => q.QuestionId);
    }
}
