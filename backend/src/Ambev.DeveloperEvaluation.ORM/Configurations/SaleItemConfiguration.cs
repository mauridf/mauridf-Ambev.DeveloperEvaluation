using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Configurations
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ProductName).IsRequired();
            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.UnitPrice).IsRequired();
            builder.Property(i => i.Discount).IsRequired();
        }
    }
}
