using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidatesExamsConfiguration : AuditableEntityConfiguration<CandidatesExams>
{
    public override void Configure(EntityTypeBuilder<CandidatesExams> builder)
    {
        builder.ToTable("CandidatesExams", "candidate");

        builder.HasOne(candidateexam => candidateexam.Candidate).WithMany(candidate => candidate.Exams).HasForeignKey(candidateexam => candidateexam.CandidateId);

        builder.HasOne(candidateexam => candidateexam.CandidateExam).WithMany(exam => exam.CandidatesExams).HasForeignKey(candidateexam => candidateexam.CandidateExamId);

        //builder.HasOne(candidateexam => candidateexam.CandidateQuestion).WithOne(exam => exam.CandidatesExams).HasForeignKey<CandidateCandidateQuestion>(candidateexam => candidateexam.CandidatesExamsId);


    }
}
