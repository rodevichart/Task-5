using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VideoRentBL.DTOs;
using VideoRentBL.Lib;
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

        public IList<ChartDetails> GetCountRentalsMovies()
        {
            //var charDetails = new ChartDetails();
            var charDetailsList = new List<ChartDetails>();
            var list = _videoRent.RentalRepository.GetAllRentalsWhithCustomersMoviesNMembershipType()
                .GroupBy(a => a.Movie.Name)
                .Select(g => new
                {
                    MovieName = g.Key,
                    MovieCount = g.Count()
                })
                .OrderBy(m => m.MovieName);
            foreach (var details  in list)
             {
                 var charDetails = new ChartDetails
                 {
                     MovieName = details.MovieName,
                     Count = details.MovieCount
                 };
                 charDetailsList.Add(charDetails);
            }         
            return charDetailsList;
        }
    }
}