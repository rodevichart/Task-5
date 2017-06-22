using System.Configuration;
using System.Data.Entity;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Persistence.EntityConfigurations;

namespace VideoRentDAL.Persistence
{
    public class VideoRentContext : DbContext
    {

        public VideoRentContext(string connString)
            : base(connString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }


        /*public VideoRentContext()
            : base(ConfigurationManager.ConnectionStrings["VideoR"].ConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }*/

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MembershipType> MembershipTypes { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerConfigurations());
            modelBuilder.Configurations.Add(new MovieConfigurations());
            modelBuilder.Configurations.Add(new MembershipConfigurations());               
            modelBuilder.Configurations.Add(new GenreConfigurations());               
        }
    }
}

