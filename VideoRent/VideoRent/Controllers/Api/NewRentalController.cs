using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using VideoRent.Models;
using VideoRent.ViewModels;
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
        [Authorize(Roles = RoleName.CanManageMoviesCustomers)]
        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRental newRental)
        {
            var custId = newRental.CustomerId;
            try
            {
                Logic.RentalService.AddRentalsForCustomer(custId, newRental.MovieIds);

                return Ok();
            }
            catch (BlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    }
}
