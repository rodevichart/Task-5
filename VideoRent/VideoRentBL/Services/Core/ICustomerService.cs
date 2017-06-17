using System.Collections.Generic;
using VideoRentBL.DTOs;
using VideoRentDAL.Core.Domain;

namespace VideoRentBL.Services.Core
{
    public interface ICustomerService : IService<Customer, CustomerDto>
    {
        IList<CustomerDto> GetCustomersWithMembershipTypeNBirthdate(int pageIndex = 1, int pageSize = 10);
        CustomerDto GetCustomerWithMembershipTypeNBirthdate(int id);
    }
}