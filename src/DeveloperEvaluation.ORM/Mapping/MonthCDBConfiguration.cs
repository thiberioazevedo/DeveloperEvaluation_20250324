using DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperEvaluation.ORM.Mapping;

public class MonthCDBConfiguration : IEntityTypeConfiguration<MonthCDB>
{
    public void Configure(EntityTypeBuilder<MonthCDB> builder)
    {
        builder.ToTable("MonthsCDBs");

        builder.HasKey(u => u.Id).HasName("PK_MonthsCDBs");

        builder.Property(u => u.Id).IsRequired();
        builder.Property(u => u.CDBId).IsRequired();
        builder.Property(u => u.Month).IsRequired();
        builder.Property(u => u.InitialValue).IsRequired();
        builder.Property(u => u.TaxPercentage).IsRequired();
        builder.Property(u => u.GrossValue).IsRequired();
        builder.Property(u => u.TaxAmount).IsRequired();
        builder.Property(u => u.NetValue).IsRequired();

        builder.HasOne(u => u.CDB).WithMany(i => i.MonthCDBCollection).HasForeignKey(i => i.CDBId).HasConstraintName("FK_MonthsCDBs_CDBs");

        builder.HasIndex(u => new { u.CDBId,u.Month }).IsUnique().HasDatabaseName("UX_MonthsCDBs_Month_CDBId").HasMethod("btree"); // PostgreSQL default method for non-clustered index;

        builder.Ignore(u => u.Number);
    }
}
