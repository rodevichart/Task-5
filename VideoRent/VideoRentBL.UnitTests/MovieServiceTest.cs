using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using VideoRentBL.DTOs;
using VideoRentBL.RoutingSolutiongConfigs;
using VideoRentBL.Services.Core;
using VideoRentBL.Services.Persistence;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using Xbehave;
using Xunit;

namespace VideoRentBL.UnitTests
{
  public class MovieServiceTest
    {

        private static IList<Movie> _listMovies;
        public MovieServiceTest()
      {
            Mapper.Initialize(cfg => { cfg.AddProfile<MappingProfile>(); });
            _listMovies = GetMovies();          
        }


        private static IList<Movie> GetMovies()
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
        public void GetCourse(MovieService movieService, MovieDto movie)
        {
            var dalMock = new Mock<IUnitOfWork>();
            dalMock.Setup(m => m.MoviesRepository.Get(It.IsAny<int>()))
                .Returns<Movie>(m => m);
              // => _listMovies.FirstOrDefault(item => item.Id == movie.Id));
         
            var blMock = new Mock<IUnitOfWorkService>();

            "Creating course service".x(() => movieService = new MovieService(dalMock.Object, dalMock.Object.MoviesRepository));

            "Getting course".x(() => movie = movieService.Get(_listMovies[0].Id));

            "Course id should be equal first element in collection".x(() => Assert.Equal(_listMovies[0].Id, movie.Id));
        }
        


    }
}
