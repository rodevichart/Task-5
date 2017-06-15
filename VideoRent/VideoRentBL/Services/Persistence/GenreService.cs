using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentBL.Services.Persistence
{
    public class GenreService:Service<Genre,GenreDto>,IGenreService
    {
        public GenreService(IUnitOfWork unitOfWork, IRepository<Genre> repository) : base(unitOfWork, repository)
        {
        }
    }
}