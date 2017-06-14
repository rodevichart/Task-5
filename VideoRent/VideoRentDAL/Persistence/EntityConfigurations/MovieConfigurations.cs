using System.Data.Entity.ModelConfiguration;
using VideoRentDAL.Core.Domain;

namespace VideoRentDAL.Persistence.EntityConfigurations
{
    public class MovieConfigurations : EntityTypeConfiguration<Movie>
    {
        public MovieConfigurations()
        {
            Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(255);

            Property(m => m.GenreId)
                .IsRequired();
        }
         
    }
}