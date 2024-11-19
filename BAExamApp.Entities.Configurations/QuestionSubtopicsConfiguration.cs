using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.Configurations;
public class QuestionSubtopicsConfiguration : AuditableEntityConfiguration<QuestionSubtopics>
{
    public override void Configure(EntityTypeBuilder<QuestionSubtopics> builder)
    {
        base.Configure(builder);
        builder.HasOne(x => x.Question).WithMany(x => x.QuestionSubtopics).HasForeignKey(x => x.QuestionId);
        builder.HasOne(x => x.Subtopic).WithMany(x => x.QuestionSubtopics).HasForeignKey(x => x.SubtopicId);
        builder.HasAlternateKey(x => new {x.QuestionId, x.SubtopicId});
    } 
}
