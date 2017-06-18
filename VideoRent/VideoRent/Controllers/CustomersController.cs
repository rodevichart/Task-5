using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
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

        // GET: Customers
        //[Route("customers")]
        public ActionResult Index()
        {
            IList<CustomerDto> customerList = Logic.CustomerService.GetCustomersWithMembershipTypeNBirthdate();
            var view = Mapper.Map<IList<CustomerDto>, IList<Customer>>(customerList);
            return View(view);
        }

        //[Route("customer/details/{id:regex(\\d{4})}")]
        public ActionResult Details(int id)
        {
            var selected = Logic.CustomerService.GetCustomerWithMembershipTypeNBirthdate(id);
            var view = Mapper.Map<CustomerDto, Customer>(selected);
            if (view == null)
                return HttpNotFound();
            return View(view);
        }

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

        [HttpPost]
        public void Delete(int id)
        {
            var customer = Logic.CustomerService.SingleOrDefault(c => c.Id == id);
            if (customer != null)
                Logic.CustomerService.Remove(customer.Id);
        }
    }
}