using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using VideoRentBL.DTOs;
using VideoRentBL.Exceptons;
using VideoRentBL.Lib;
using VideoRentBL.RoutingSolutiongConfigs;
using VideoRentBL.Services.Persistence;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using Xbehave;
using Xunit;

namespace VideoRentBL.UnitTest
{
    public class RentalServiceTest
    {
        private readonly IList<Customer> _listOfCustomers;
        private readonly IList<Movie> _listMovies;
        private readonly IList<Rental> _listRentals;
        public RentalServiceTest()
        {
            Mapper.Initialize(cfg => { cfg.AddProfile<MappingProfile>(); });
            _listOfCustomers = GetCustomers();
            _listMovies = GetMovies();
            _listRentals = GetRentals();
        }

        private  IList<Rental> GetRentals()
        {
            return new List<Rental>
            {
                new Rental
                {
                    Id = 1,
                    MovieId = 1,
                    Movie = _listMovies[0],
                    CustomerId = 2,
                    DateRented = DateTime.Now
                },

                new Rental
                {
                    Id = 2,
                    MovieId = 2,
                    Movie = _listMovies[1],
                    CustomerId = 1,
                    DateRented = DateTime.Now
                }
            };
        }



        private  IList<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Alevtina"                    
                },

                new Customer
                {
                    Id = 2,
                    Name = "Andreika"
                }
            };
        }


        private  IList<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Name = "Matrix",
                    GenreId = 2,
                    NumberInStock = 5,
                    NumberAvailable = 3,
                    ReleaseDate = new DateTime(2000,11,13),
                    DateAdded = new DateTime(2012,08,22)
                },
                new Movie
                {
                    Id =2,
                    Name = "Lord of the Rings",
                    GenreId = 1,
                    NumberInStock = 3,
                    NumberAvailable = 0,
                    ReleaseDate = new DateTime(2008,11,13),
                    DateAdded = new DateTime(2014,01,22)

                }
            };
        }


        [Scenario]
        public void AddRentalsForCustomer_GetClient_ThrowBlException(RentalService rentalService)
        {
            var dalMock = new Mock<IUnitOfWork>();
            dalMock.Setup(m => m.RentalRepository.Get(It.IsAny<int>()))
                .Returns<int>(m => _listRentals.FirstOrDefault(l => l.Id == m));

            dalMock.Setup(m => m.CustomersRepository.Get(It.IsAny<int>()))
                .Returns(null as Customer);

            "Creating Rental service".x(() => rentalService = new RentalService(dalMock.Object, dalMock.Object.RentalRepository));

            "We should get Customer-null BlException".x(() => Assert.Throws<BlException>(() => rentalService.AddRentalsForCustomer(1, new List<int>()))); 
        }

        [Scenario]
        public void AddRentalsForCustomer_GetMovie_ThrowBlException(RentalService rentalService)
        {
            var dalMock = new Mock<IUnitOfWork>();
            dalMock.Setup(m => m.RentalRepository.Get(It.IsAny<int>()))
                .Returns<int>(m => _listRentals.FirstOrDefault(l => l.Id == m));

            dalMock.Setup(m => m.CustomersRepository.Get(It.IsAny<int>()))
                .Returns<int>(m => _listOfCustomers.FirstOrDefault(l => l.Id == m));

            dalMock.Setup(m => m.MoviesRepository.Get(It.IsAny<int>()))
                .Returns(null as Movie);

            "Creating Rental service".x(() => rentalService = new RentalService(dalMock.Object, dalMock.Object.RentalRepository));

            "We should get Movie-null BlException".x(() => Assert.Throws<BlException>(() => rentalService.AddRentalsForCustomer(1, new List<int> {1}))); 
        }


      
                            
        [Scenario]
        public void AddRentalsForCustomer_GetMovieNumberInStock_ThrowNoMovieException(RentalService rentalService, Movie movie)
        {
            var dalMock = new Mock<IUnitOfWork>();
            dalMock.Setup(m => m.MoviesRepository.Get(It.IsAny<int>()))
                .Returns<int>(m => _listMovies.FirstOrDefault(l => l.NumberAvailable == 0));

            dalMock.Setup(m => m.CustomersRepository.Get(It.IsAny<int>()))
                .Returns<int>(m => _listOfCustomers.FirstOrDefault(l => l.Id == m));

            "Creating Rental service".x(() => rentalService = new RentalService(dalMock.Object, dalMock.Object.RentalRepository));

            "We should get Number In Stock - 0 NoMovieException".x(() => Assert.Throws<NoMovieException>(() =>
                                rentalService.AddRentalsForCustomer(1, new List<int> {2})));
        }


        [Scenario]
        public void AddRentalForCustomer(Customer customer, RentalService rentalService)
        {
            var dalMock = new Mock<IUnitOfWork>();
            dalMock.Setup(m => m.RentalRepository.Add(It.IsAny<Rental>()))
                .Callback((Rental rent) => _listRentals.Add(rent));

            dalMock.Setup(m => m.MoviesRepository.Get(It.IsAny<int>()))
                .Returns<int>(m => _listMovies.FirstOrDefault(l => l.NumberAvailable > 0));

            dalMock.Setup(m => m.CustomersRepository.Get(It.IsAny<int>()))
                .Returns<int>(m => _listOfCustomers.FirstOrDefault(l => l.Id == m));

            "Creating Rental service".x(() => rentalService = new RentalService(dalMock.Object, dalMock.Object.RentalRepository));

            "Adding Rental into Rental repository".x(() => rentalService.AddRentalsForCustomer(1, new List<int> {1}));

            "We should get 3 Rentals".x(() => Assert.Equal(3, _listRentals.Count));

            "We should get Number of available movies - 2".x(() => Assert.Equal(2, _listMovies[0].NumberAvailable));
        }

        [Scenario]
        public void GetCountRentalsMovies(RentalService rentalService, IList<ChartDetails> listOfChartDetails)
        {
            var dalMock = new Mock<IUnitOfWork>();
            dalMock.Setup(m => m.RentalRepository.GetAllRentalsWhithCustomersMoviesNMembershipType())
                .Returns(_listRentals);

            "Creating Rental service".x(() => rentalService = new RentalService(dalMock.Object, dalMock.Object.RentalRepository));

            "We get Chart Details".x(() => listOfChartDetails = rentalService.GetCountRentalsMovies());

            "We should get 22 Char Details".x(() => Assert.Equal(2, listOfChartDetails.Count));

        }

        [Scenario]
        public void GetAllRentalsWhithCustomersMoviesNMembershipType(RentalService rentalService, IList<RentalDto> rentaList)
        {
            const string orderDir = "asc";
            var dalMock = new Mock<IUnitOfWork>();
            int totalRecords;
            int recordsSearched;
     
            dalMock.Setup(m => m.RentalRepository
                .GetAllRentalsWhithCustomersMoviesNMembershipType
                            (It.IsAny<string>(), It.IsAny<int>(), orderDir, out totalRecords, out recordsSearched, It.IsAny<int>(), It.IsAny<int>()))
                                 .Returns(_listRentals);

            "Creating Rental service".x(() => rentalService = new RentalService(dalMock.Object, dalMock.Object.RentalRepository));

            "We should get All Rentals".x(() => rentaList = rentalService
                .GetAllRentalsWhithCustomersMoviesNMembershipType
                         (It.IsAny<string>(), It.IsAny<int>(), orderDir, out totalRecords, out recordsSearched));

            "We should get 2 Rentals".x(() => Assert.Equal(2, rentaList.Count));
        }
    }
}