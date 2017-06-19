using System.Collections.Generic;
using AutoMapper;
using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentBL.Services.Persistence
{
    public class CustomerService: Service<Customer,CustomerDto>,ICustomerService
    {
        private readonly IUnitOfWork _videoRent;
        public CustomerService(IUnitOfWork unitOfWork, IRepository<Customer> repository) : base(unitOfWork, repository)
        {
            _videoRent = unitOfWork;
        }

        public IList<CustomerDto> GetCustomersWithMembershipTypeNBirthdate(string search, int orderColm, string orderDir, out int totalRecords, out int recordsSearched, int pageIndex = 1, int pageSize = 10)
        {
           
            return Mapper.Map<IList<Customer>, IList<CustomerDto>>
                (_videoRent.CustomersRepository.GetCustomersWithMembershipTypeNBirthdate(search, orderColm, orderDir, out totalRecords, out recordsSearched, pageIndex,pageSize));
        }

        public CustomerDto GetCustomerWithMembershipTypeNBirthdate(int id)
        {
            return Mapper.Map<Customer, CustomerDto>(_videoRent.CustomersRepository.GetCustomerWithMembershipTypeNBirthdate(id));
        }
    }
}