using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentBL.Services.Persistence
{
    public class MovieService : Service<Movie,MovieDto>,IMovieService
    {
        private readonly IUnitOfWork _videoRent;
        public MovieService(IUnitOfWork unitOfWork, IRepository<Movie> repository) : base(unitOfWork, repository)
        {
            _videoRent = unitOfWork;
        }
    }
}