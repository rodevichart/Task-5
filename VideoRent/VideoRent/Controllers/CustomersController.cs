using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VideoRent.Models;
using VideoRent.Models.JsonDatatables;
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
   
        [Route("GetCustomers")]
        public ActionResult GetCustomers(string query = null)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                var customers = Logic.CustomerService.Find(c => c.Name.Contains(query));
                var json = (new CamelCaseResolver
                {
                    Data = customers,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                });
                return json;
            }

            return HttpNotFound("Customer not founded");
        }

        [Authorize(Roles = RoleName.CanManageMoviesCustomers)]
        public ActionResult New()
        {
            var membershipTypeList = Logic.MembershipTypeService.GetAll();
            var membershipTypeListView =
                Mapper.Map<IEnumerable<MembershipTypeDto>, IEnumerable<MembershipType>>(membershipTypeList);

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypeListView,
                Customer = new Customer()
            };
            return View("CustomerForm", viewModel);
        }


        [Authorize(Roles = RoleName.CanManageMoviesCustomers)]
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
                Logic.CustomerService.Update(Mapper.Map<Customer, CustomerDto>(customerFormViewModel.Customer));
            }


            return RedirectToAction("Index", "Customers");
        }

        // GET: Customers/Index
        //[Route("customers")]        
        [HttpGet]
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMoviesCustomers))
                return View("List");
            return View("ReadOnlyList");
        }

        [HttpPost]
        public ActionResult Index(DataTableAjaxPostModel model)
        {
            if (model == null)
                return HttpNotFound();

            int totalRecords;
            int recordsSearched;

            var orderColm = model.order.ElementAtOrDefault(0)?.column ?? 0;
            var orderDir = model.order?.ElementAtOrDefault(0)?.dir;
            var start = model.start.HasValue ? model.start / 10 : 0;

            var customerList =
                Logic.CustomerService.GetCustomersWithMembershipTypeNBirthdate(model.search.value, orderColm, orderDir,
                    out totalRecords, out recordsSearched, start.Value, model.length ?? 10);
            var view = Mapper.Map<IList<CustomerDto>, IList<Customer>>(customerList);

            var json = (new CamelCaseResolver
            {
                Data = new {draw = model.draw, recordsFiltered = recordsSearched, recordsTotal = totalRecords, data = view},
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
        [Authorize(Roles = RoleName.CanManageMoviesCustomers)]
        public ActionResult Edit(int id)
        {
            var customer = Mapper.Map<CustomerDto, Customer>(Logic.CustomerService.SingleOrDefault(c => c.Id == id));

            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes =
                    Mapper.Map<IEnumerable<MembershipTypeDto>, IEnumerable<MembershipType>>(
                        Logic.MembershipTypeService.GetAll())
            };

            return View("CustomerForm", viewModel);
        }

        // POST: Customers/Delete/id
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMoviesCustomers)]
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