using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configurations
{
    public class UserOperationClaimEntityConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.HasIndex(o => o.UserId);

            builder.Property(o => o.UserId)
                .IsRequired();

            builder.HasIndex(o => o.OperationClaimId);

            builder.Property(o => o.OperationClaimId)
                .IsRequired();
        }
    }
}
