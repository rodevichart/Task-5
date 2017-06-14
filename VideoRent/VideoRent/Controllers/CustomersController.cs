using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoRent.Models;

namespace VideoRent.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        //[Route("customers")]
        public ActionResult Index()
        {
            var customerList = new List<Customer>
            {
                new Customer {Id = 1,Name = "Artsem"},
                new Customer {Id = 2,Name = "Valiont"}
            };
            

            return View(customerList);
        }

        //[Route("customer/details/{id:regex(\\d{4})}")]
        public ActionResult Details(int? id)
        {
            var customerList = new List<Customer>
            {
                new Customer {Id = 1,Name = "Artsem"},
                new Customer {Id = 2,Name = "Valiont"}
            };

            var selected = customerList.SingleOrDefault(m => m.Id == id);
            if (selected == null)
                return HttpNotFound();
            return View(selected);
        }
    }
}