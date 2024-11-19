using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidateExamConfiguration : AuditableEntityConfiguration<CandidateExam>
{
    public override void Configure(EntityTypeBuilder<CandidateExam> builder)
    {
        base.Configure(builder);


        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .HasMaxLength(30)
               .IsRequired();

        builder.ToTable("CandidateExams", "candidate");
    }
}
