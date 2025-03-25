using DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperEvaluation.ORM.Mapping;

public class CDBConfiguration : IEntityTypeConfiguration<CDB>
{
    public void Configure(EntityTypeBuilder<CDB> builder)
    {
        builder.ToTable("CDBs");

        builder.HasKey(u => u.Id).HasName("PK_CDBs");

        builder.Property(u => u.Id).IsRequired();
        builder.Property(u => u.Value).IsRequired();
        builder.Property(u => u.Months).IsRequired();
        builder.Property(u => u.CDI).IsRequired();
        builder.Property(u => u.TB).IsRequired();
        builder.Property(u => u.GrossValue).IsRequired();
        builder.Property(u => u.TaxPercentage).IsRequired();
        builder.Property(u => u.TaxAmount).IsRequired();
        builder.Property(u => u.NetValue).IsRequired();
    }
}
