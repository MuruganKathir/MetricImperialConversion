using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conversion.DataAccess
{
    public class ConversionHistoryConfiguration : IEntityTypeConfiguration<ConversionHistory>
    {
        public void Configure(EntityTypeBuilder<ConversionHistory> entity)
        {
            entity.HasKey(e => e.HistoryId);

            entity.Property(e => e.UserId);

            entity.Property(e => e.ConversionType)
                  .IsRequired()
                  .HasMaxLength(100)
                  .IsUnicode(false);

            entity.Property(e => e.ConversionFrom)
                  .IsRequired()
                  .HasMaxLength(100)
                  .IsUnicode(false);

            entity.Property(e => e.ConversionTo)
                  .IsRequired()
                  .HasMaxLength(100)
                  .IsUnicode(false);

            entity.Property(e => e.ValueToConvert)
                  .HasColumnType("decimal(18, 4)");

            entity.Property(e => e.ConvertedResult)
                  .HasColumnType("decimal(18, 4)");

            entity.Property(e => e.CreatedAt)
                  .HasColumnType("datetime");


            entity.HasOne(e => e.User)
                  .WithMany(t => t.ConversionHistorys)
                  .HasForeignKey(e => e.UserId)
                  .HasConstraintName("FK_ConversionHistory_User");

        }
    }
}