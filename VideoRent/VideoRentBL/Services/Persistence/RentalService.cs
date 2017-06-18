using System.Collections.Generic;
using AutoMapper;
using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentBL.Services.Persistence
{
    public class RentalService : Service<Rental, RentalDto>, IRentalService
    {
        private readonly IUnitOfWork _videoRent;
        public RentalService(IUnitOfWork unitOfWork, IRepository<Rental> repository) : base(unitOfWork, repository)
        {
            _videoRent = unitOfWork;
        }

        public IList<RentalDto> GetAllRentalsWhithCustomersMoviesNMembershipType(int pageIndex = 1, int pageSize = 10)
        {
           return Mapper.Map<IList<Rental>, IList<RentalDto>>(_videoRent.RentalRepository.GetAllRentalsWhithCustomersMoviesNMembershipType(pageIndex,pageSize));
        }
    }
}