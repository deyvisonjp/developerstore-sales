using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.UnitPrice).HasPrecision(18, 2);

            builder
                .HasOne(x => x.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(x => x.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder
            //    .HasOne(x => x.ProductId)
            //    .WithMany(p => p.SaleItems)
            //    .HasForeignKey(x => x.ProductId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
