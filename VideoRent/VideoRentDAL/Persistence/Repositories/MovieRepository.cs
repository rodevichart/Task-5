using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Persistence.Repositories
{
    public class MovieRepository:Repository<Movie>,IMovieRepository
    {
        public MovieRepository(DbContext context) : base(context)
        {
        }

        public VideoRentContext VideoRentContext => Context as VideoRentContext;
        public List<Movie> GetCustomersWithMembershipTypeNBirthdate(int pageIndex = 1, int pageSize = 15)
        {
            
                return VideoRentContext.Movies
                .Include(m => m.Genre)
                .OrderBy(c => c.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Movie GetCustomerWithMembershipTypeNBirthdate(int id)
        {
            return VideoRentContext.Movies
                .Include(m => m.Genre)
                .SingleOrDefault(c => c.Id == id); ;
        }
    }
}