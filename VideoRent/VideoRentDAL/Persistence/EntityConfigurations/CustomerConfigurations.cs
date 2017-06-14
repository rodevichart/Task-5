using System.Data.Entity.ModelConfiguration;
using VideoRentDAL.Core.Domain;

namespace VideoRentDAL.Persistence.EntityConfigurations
{
    public class CustomerConfigurations : EntityTypeConfiguration<Customer>
    {
        public CustomerConfigurations()
        {
            
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            HasRequired(c => c.MembershipType)
                .WithMany(m => m.Customers)
                .HasForeignKey(c => c.MembershipTypeId)
                .WillCascadeOnDelete(false);
         
        }
    }
}

