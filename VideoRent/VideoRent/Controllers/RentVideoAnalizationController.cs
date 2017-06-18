using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using VideoRent.Models;
using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;

namespace VideoRent.Controllers
{
    public class RentVideoAnalizationController : AbastractController
    {
        public RentVideoAnalizationController(IUnitOfWorkService logic) : base(logic)
        {
        }
        // GET: RentVideoAnalization
        public ActionResult Index()
        {
            var rentalList = Logic.RentalService.GetAllRentalsWhithCustomersMoviesNMembershipType(1);
            //var addMembershipType = rentalList.ForEach(r => r.Customer.MembershipType.Name = )
            var view = Mapper.Map<IList<RentalDto>, IList<Rental>>(rentalList);
            return View(view);
        }
    }
}