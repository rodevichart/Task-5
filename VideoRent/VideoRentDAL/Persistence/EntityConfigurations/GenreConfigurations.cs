using System.Data.Entity.ModelConfiguration;
using VideoRentDAL.Core.Domain;

namespace VideoRentDAL.Persistence.EntityConfigurations
{
    public class GenreConfigurations : EntityTypeConfiguration<Genre>
    {
        public GenreConfigurations()
        {
            Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
         
    }
}