using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VideoRent.Models;
using VideoRentBL.DTOs;
using VideoRentBL.Persistence;
using WebGrease.Css.Extensions;

namespace VideoRent.Controllers
{
    public class RentVideoAnalizationController : Controller
    {
        private VideoRentCore VideoRentCore { get; set; }

        public RentVideoAnalizationController()
        {
            VideoRentCore = new VideoRentCore();
        }
        // GET: RentVideoAnalization
        public ActionResult Index()
        {
            var rentalList = VideoRentCore.UnitService.RentalService.GetAllRentalsWhithCustomersMoviesNMembershipType(1);
            //var addMembershipType = rentalList.ForEach(r => r.Customer.MembershipType.Name = )
            var view = Mapper.Map<IList<RentalDto>, IList<Rental>>(rentalList);
            return View(view);
        }
    }
}