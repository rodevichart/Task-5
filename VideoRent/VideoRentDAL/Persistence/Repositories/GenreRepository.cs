using System.Data.Entity;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Persistence.Repositories
{
    public class GenreRepository:Repository<Genre>,IGenreRepository
    {
        public GenreRepository(DbContext context) : base(context)
        {
        }
    }
}