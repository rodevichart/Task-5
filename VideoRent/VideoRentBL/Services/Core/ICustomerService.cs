using System.Collections.Generic;
using VideoRentBL.DTOs;

namespace VideoRentBL.Services.Core
{
    public interface ICustomerService : IService<CustomerDto>
    {
        IList<CustomerDto> GetCustomersWithMembershipTypeNBirthdate(int pageIndex = 1, int pageSize = 10);
        CustomerDto GetCustomerWithMembershipTypeNBirthdate(int id);
    }
}