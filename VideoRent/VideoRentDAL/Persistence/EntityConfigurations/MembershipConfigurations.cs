using System.Data.Entity.ModelConfiguration;
using VideoRentDAL.Core.Domain;

namespace VideoRentDAL.Persistence.EntityConfigurations
{
    public class MembershipConfigurations : EntityTypeConfiguration<MembershipType>
    {
        public MembershipConfigurations()
        {
            Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(255);

            Property(m => m.SingUpFree)
                .HasColumnName("SingUpFee");
        }
         
    }
}