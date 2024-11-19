using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.Entities.Configurations.Candidate;

public class CandidatesGroupsConfiguration : AuditableEntityConfiguration<CandidatesGroups>
{
    public override void Configure(EntityTypeBuilder<CandidatesGroups> builder)
    {
        builder.ToTable("CandidatesGroups", "candidate");
        //builder.HasKey(cg => new { cg.CandidateGroupId, cg.CandidateId });
        //builder.HasNoKey();

        builder.HasOne(candidategroup => candidategroup.CandidateGroup).WithMany(group => group.Candidates).HasForeignKey(candidategroup => candidategroup.CandidateGroupId);

        builder.HasOne(candidategroup => candidategroup.Candidate).WithMany(candidate => candidate.Groups).HasForeignKey(candidategroup => candidategroup.CandidateId);

    }
}
