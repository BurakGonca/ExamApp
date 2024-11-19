using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.Configurations;
public class RegisterCodeConfiguration:AuditableEntityConfiguration<RegisterCode>
{
    public override void Configure(EntityTypeBuilder<RegisterCode> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Code).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
        builder.Property(x=>x.CodeCreationTime).HasColumnType("datetime2").IsRequired();
        builder.Property(x=>x.CodeExpirationTime).HasColumnType("datetime2").IsRequired();
        builder.Property(x=>x.CreatedForId).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();

    }
}
