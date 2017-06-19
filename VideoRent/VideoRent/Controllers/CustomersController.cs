using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VideoRent.Models;
using VideoRent.ViewModels;
using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;

namespace VideoRent.Controllers
{
    public class CustomersController : AbastractController
    {
        public CustomersController(IUnitOfWorkService logic) : base(logic)
        {
        }


        public ActionResult New()
        {
            var membershipTypeList = Logic.MembershipTypeService.GetAll();
            var membershipTypeListView = Mapper.Map<IEnumerable<MembershipTypeDto>, IEnumerable<MembershipType>>(membershipTypeList);

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypeListView,
                Customer = new Customer()
            };
            return View("CustomerForm",viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel customerFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var veiwModel = new CustomerFormViewModel
                {
                    Customer = customerFormViewModel.Customer,
                    MembershipTypes =
                        Mapper.Map<IEnumerable<MembershipTypeDto>, IEnumerable<MembershipType>>(
                            Logic.MembershipTypeService.GetAll())
                };
                return View("CustomerForm", veiwModel);
            }

            if (customerFormViewModel.Customer.Id == 0)
                Logic.CustomerService.Add(
                    Mapper.Map<Customer, CustomerDto>(customerFormViewModel.Customer));
            else
            {
                Logic.CustomerService.Update(Mapper.Map<Customer,CustomerDto>(customerFormViewModel.Customer));
            }


            return RedirectToAction("Index", "Customers");
        }

        // GET: Customers/Index
        //[Route("customers")]        
        [HttpGet]
        public ActionResult Index()
        {         
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? draw, int? start, int? length)
        {
            var totalRecords = 0;
            var recordsSearched = 0;
            var orderColm = 0;
            
            int.TryParse(Request.Form.GetValues("order[0][column]")?.ElementAtOrDefault(0), out orderColm);
            var orderDir = Request.Form.GetValues("order[0][dir]")?.ElementAtOrDefault(0);
            var search = Request["search[value]"];            
            start = start.HasValue ? start/10 : 0;
            var customerList = 
                Logic.CustomerService.GetCustomersWithMembershipTypeNBirthdate(search, orderColm, orderDir, out totalRecords, out recordsSearched, start.Value, length ?? 10);
            var view = Mapper.Map<IList<CustomerDto>, IList<Customer>>(customerList);

            var json = (new CamelCaseResolver
            {
                Data = new { draw = draw, recordsFiltered = recordsSearched, recordsTotal = totalRecords, data = view },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });          
            return json;
        }

        //[Route("customer/details/{id:regex(\\d{4})}")]
        // GET: Customers/Details
        public ActionResult Details(int id)
        {
            var selected = Logic.CustomerService.GetCustomerWithMembershipTypeNBirthdate(id);
            var view = Mapper.Map<CustomerDto, Customer>(selected);
            if (view == null)
                return HttpNotFound();
            return View(view);
        }

        // GET: Customers/Edit/id
        public ActionResult Edit(int id)
        {
            var customer = Mapper.Map<CustomerDto, Customer>(Logic.CustomerService.SingleOrDefault(c => c.Id == id));

            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = Mapper.Map<IEnumerable<MembershipTypeDto>,IEnumerable<MembershipType>>(Logic.MembershipTypeService.GetAll())
            }; 

            return View("CustomerForm",viewModel);
        }

        // POST: Customers/Delete/id
        [HttpPost]
        public HttpStatusCode Delete(int id)
        {
            var customer = Logic.CustomerService.SingleOrDefault(c => c.Id == id);
            if (customer != null)
            {
                Logic.CustomerService.Remove(customer.Id);
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }
    }
}