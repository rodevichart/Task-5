using System.Data.Entity;
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
    }
}