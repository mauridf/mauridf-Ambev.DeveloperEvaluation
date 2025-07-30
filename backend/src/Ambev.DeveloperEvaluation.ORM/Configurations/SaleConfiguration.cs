using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SaleNumber).IsRequired();
            builder.Property(s => s.ClientName).IsRequired();
            builder.Property(s => s.BranchName).IsRequired();
            builder.Property(s => s.SaleDate).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.HasMany(s => s.Items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
