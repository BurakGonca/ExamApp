using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidateQuestionConfiguration : AuditableEntityConfiguration<CandidateQuestion>
{
    public override void Configure(EntityTypeBuilder<CandidateQuestion> builder)
    {
        base.Configure(builder);

        builder.ToTable("CandidateQuestions", "candidate");

        builder.Property(x => x.Content).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(x => x.CandidateQuestionType).IsRequired();
        builder.Property(x => x.Image).IsRequired(false).HasColumnType("nvarchar(max)");
        builder.Property(x => x.IsActive).IsRequired();

    }
}
