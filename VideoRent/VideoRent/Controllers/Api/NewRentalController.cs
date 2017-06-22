using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using VideoRent.Models;
using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;

namespace VideoRent.Controllers.Api
{
    public class NewRentalController : AbstractApiController
    {
        public NewRentalController(IUnitOfWorkService logic) : base(logic)
        {
        }

        public NewRentalController()
        {            
        }

        [HttpPost]
        //[Route("newrental")]
        public IHttpActionResult CreateNewRental(NewRental newRental)
        {
            var custId = newRental.CustomerId;
            var customer = Mapper.Map<CustomerDto, Customer>(Logic.CustomerService.Get(custId));

            //if (customer == null)
            //    return BadRequest("Invalid customer Id.");

            var movies = Mapper.Map<IEnumerable<MovieDto>, IEnumerable<Movie>>(Logic.MovieService.Find(m => newRental.MovieIds.Contains(m.Id)));

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                Logic.MovieService.Update(Mapper.Map<Movie, MovieDto>(movie));
                Logic.RentalService.Add(Mapper.Map<Rental, RentalDto>(rental));
            }

            return Ok();
        }

        public IHttpActionResult GetCust()
        {
            return Ok();
        }


    }
}
