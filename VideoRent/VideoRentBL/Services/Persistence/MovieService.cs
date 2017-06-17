using System.Collections.Generic;
using AutoMapper;
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

        public IList<MovieDto> GetCustomersWithMembershipTypeNBirthdate(int pageIndex = 1, int pageSize = 10)
        {
            return Mapper.Map<IList<Movie>, IList<MovieDto>>(_videoRent.MoviesRepository.GetCustomersWithMembershipTypeNBirthdate(pageIndex, pageSize));
        }

        public MovieDto GetCustomerWithMembershipTypeNBirthdate(int id)
        {
            return Mapper.Map<Movie, MovieDto>(_videoRent.MoviesRepository.GetCustomerWithMembershipTypeNBirthdate(id));
        }
    }
}