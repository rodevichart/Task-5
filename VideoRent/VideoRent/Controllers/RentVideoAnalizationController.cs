using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using VideoRent.Models;
using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;

namespace VideoRent.Controllers
{
    [Authorize(Roles = RoleName.CanManageMoviesCustomers)]
    public class RentVideoAnalizationController : AbastractController
    {
        public RentVideoAnalizationController(IUnitOfWorkService logic) : base(logic)
        {
        }
        // GET: RentVideoAnalization
        public ActionResult Index()
        {
            var rentalList = Logic.RentalService.GetAllRentalsWhithCustomersMoviesNMembershipType(1);          
            var view = Mapper.Map<IList<RentalDto>, IList<Rental>>(rentalList);          
            return View(view);
        }

        public ActionResult GetCharData()
        {
            var charData = Logic.RentalService.GetCountRentalsMovies();
            var serialize = JsonConvert.SerializeObject(charData, Formatting.Indented);
            var json = (new CamelCaseResolver
            {
                Data = charData,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });
            return json;
        }
    }
}