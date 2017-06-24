using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VideoRentBL.DTOs;
using VideoRentBL.Exceptons;
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

       
        public IList<RentalDto> GetAllRentalsWhithCustomersMoviesNMembershipType(string search, int orderColm,
            string orderDir,
            out int totalRecords, out int recordSearched, int pageIndex = 1, int pageSize = 10)
        {
            return
                Mapper.Map<IList<Rental>, IList<RentalDto>>(
                    _videoRent.RentalRepository.GetAllRentalsWhithCustomersMoviesNMembershipType(search, orderColm,
                        orderDir,
                        out totalRecords, out recordSearched, pageIndex, pageSize));
        }

        public IList<ChartDetails> GetCountRentalsMovies()
        {            
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

        public void AddRentalsForCustomer(int customerId, IEnumerable<int> moviesIds)
        {
            try
            {
                var customer = _videoRent.CustomersRepository.Get(customerId);
                if (customer == null)
                    throw new NullReferenceException("Can not find customer.");

                foreach (var movieId in moviesIds)
                {
                    var movie = _videoRent.MoviesRepository.Get(movieId);
                    if (movie == null)
                        throw new NullReferenceException("Can not find movie.");

                    if (movie.NumberInStock == 0)
                        throw new NoMovieException("No movie in stock.");

                    movie.NumberInStock--;

                    _videoRent.RentalRepository.Add(new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.Now
                    });
                }

                _videoRent.Complete();
            }
            catch (NullReferenceException ex)
            {                
                throw new BlException(ex.Message,ex.InnerException);
            }
            
        }
    }
}