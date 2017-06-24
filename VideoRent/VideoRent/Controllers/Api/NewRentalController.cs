using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using VideoRent.Models;
using VideoRentBL.DTOs;
using VideoRentBL.Exceptons;
using VideoRentBL.Services.Core;

namespace VideoRent.Controllers.Api
{
    public class NewRentalController : AbstractApiController
    {
        public NewRentalController(IUnitOfWorkService logic) : base(logic)
        {
        }

        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRental newRental)
        {
            var custId = newRental.CustomerId;
            try
            {
                Logic.RentalService.AddRentalsForCustomer(custId, newRental.MovieIds);

                return Ok();
            }
            catch (NoMovieException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                Logic.Dispose();
            }
            
        }

    }
}
