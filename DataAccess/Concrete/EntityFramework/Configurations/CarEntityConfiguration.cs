using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configurations
{
    public class CarEntityConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.HasIndex(c => new { c.BrandId, c.ColorId });

            builder.Property(c => c.BrandId).IsRequired();

            builder.Property(c => c.ColorId).IsRequired();

            builder.Property(c => c.ModelYear).IsRequired();

            builder.Property(c => c.DailyPrice).IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
