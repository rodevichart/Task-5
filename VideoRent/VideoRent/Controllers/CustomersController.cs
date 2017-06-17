using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VideoRent.Models;
using VideoRent.ViewModels;
using VideoRentBL.DTOs;
using VideoRentBL.Persistence;

namespace VideoRent.Controllers
{
    public class CustomersController : Controller
    {
        private VideoRentCore VideoRentCore { get; set; }
         
        public CustomersController()
        {            
            VideoRentCore = new VideoRentCore();
        }


        public ActionResult New()
        {
            var membershipTypeList = VideoRentCore.UnitService.MembershipTypeService.GetAll();
            var membershipTypeListView = Mapper.Map<IEnumerable<MembershipTypeDto>, IEnumerable<MembershipType>>(membershipTypeList);

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypeListView
            };
            return View("CustomerForm",viewModel);
        }


        [HttpPost]
        public ActionResult Save(CustomerFormViewModel customerFormViewModel )
        {
            if (customerFormViewModel.Customer.Id == 0)
                VideoRentCore.UnitService.CustomerService.Add(Mapper.Map<Customer, CustomerDto>(customerFormViewModel.Customer));
            else
            {
                var customer = VideoRentCore.UnitService.CustomerService.SingleOrDefault(
                    c => c.Id == customerFormViewModel.Customer.Id);
                            VideoRentCore.UnitService.CustomerService.Update(Mapper.Map(customerFormViewModel.Customer, customer));                
            }
            
            
            return RedirectToAction("Index", "Customers");
        }

        // GET: Customers
        //[Route("customers")]
        public ActionResult Index()
        {
            IList<CustomerDto> customerList = VideoRentCore.UnitService.CustomerService.GetCustomersWithMembershipTypeNBirthdate();
            var view = Mapper.Map<IList<CustomerDto>, IList<Customer>>(customerList);
            return View(view);
        }

        //[Route("customer/details/{id:regex(\\d{4})}")]
        public ActionResult Details(int id)
        {
            var selected = VideoRentCore.UnitService.CustomerService.GetCustomerWithMembershipTypeNBirthdate(id);
            var view = Mapper.Map<CustomerDto, Customer>(selected);
            if (view == null)
                return HttpNotFound();
            return View(view);
        }

        public ActionResult Edit(int id)
        {
            var customer = Mapper.Map<CustomerDto, Customer>(VideoRentCore.UnitService.CustomerService.SingleOrDefault(c => c.Id == id));

            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = Mapper.Map<IEnumerable<MembershipTypeDto>,IEnumerable<MembershipType>>(VideoRentCore.UnitService.MembershipTypeService.GetAll())
            }; 

            return View("CustomerForm",viewModel);
        }
    }
}