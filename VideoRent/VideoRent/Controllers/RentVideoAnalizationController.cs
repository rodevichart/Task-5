using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using VideoRent.Models;
using VideoRent.Models.JsonDatatables;
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
            return View();
        }   

        [HttpPost]
        public ActionResult Index(DataTableAjaxPostModel model)
        {
            int totalRecords;
            int recordsSearched;

            var orderColm = model.order.ElementAtOrDefault(0)?.column ?? 0;
            var orderDir = model.order?.ElementAtOrDefault(0)?.dir;
            var start = model.start.HasValue ? model.start / 10 : 0;

            var rentalList =
                Logic.RentalService.GetAllRentalsWhithCustomersMoviesNMembershipType(model.search.value, orderColm, orderDir,
                    out totalRecords, out recordsSearched, start.Value, model.length ?? 10);
            var view = Mapper.Map<IList<RentalDto>, IList<Rental>>(rentalList);

            var json = (new CamelCaseResolver
            {
                Data = new { draw = model.draw, recordsFiltered = recordsSearched, recordsTotal = totalRecords, data = view },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });
            return json;
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