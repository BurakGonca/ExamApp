using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidateCandidateQuestionConfiguration : AuditableEntityConfiguration<CandidateCandidateQuestion>
{
    public override void Configure(EntityTypeBuilder<CandidateCandidateQuestion> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.MaxScore).IsRequired();
        builder.Property(x => x.Score).IsRequired(false);
        builder.Property(x => x.QuestionOrder).IsRequired();
        builder.Property(x => x.TimeStarted).IsRequired(false);
        builder.Property(x => x.TimeFinished).IsRequired(false);
        builder.ToTable("CandidateCandidateQuestions", "candidate");

        builder.HasOne(x => x.CandidateAnswer)
       .WithOne(x => x.CandidateQuestion)
       .HasForeignKey<CandidateCandidateAnswer>(x => x.CandidateQuestionId);

       // builder.HasOne(x => x.CandidatesExams)
       //.WithOne(x => x.CandidateQuestion)
       //.HasForeignKey<CandidatesExams>(x => x.CandidateQuestionId);

    }
}
