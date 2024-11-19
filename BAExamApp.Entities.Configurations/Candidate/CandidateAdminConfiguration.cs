using BAExamApp.Entities.DbSets.Candidates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.Configurations.Candidate;
public class CandidateAdminConfiguration : BaseUserConfiguration<CandidateCandidateAdmin>
{
    public override void Configure(EntityTypeBuilder<CandidateCandidateAdmin> builder)
    {
        base.Configure(builder);
        builder.ToTable("CandidateAdmins", "candidate");
    }
}
