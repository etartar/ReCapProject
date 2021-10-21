using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configurations
{
    public class RentalEntityConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.HasIndex(r => new { r.CarId, r.CustomerId });

            builder.Property(r => r.CarId).IsRequired();

            builder.Property(r => r.CustomerId).IsRequired();

            builder.Property(r => r.RentDate).IsRequired();

            builder.Property(r => r.ReturnDate).IsRequired(false);
        }
    }
}
