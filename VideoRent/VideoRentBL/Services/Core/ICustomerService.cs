using System.Collections.Generic;
using VideoRentBL.DTOs;

namespace VideoRentBL.Services.Core
{
    public interface ICustomerService : IService<CustomerDto>
    {
        IList<CustomerDto> GetCustomersWithMembershipTypeNBirthdate(string search, int orderColm, string orderDir, out int totalRecords, out int recordsSearched, int pageIndex = 1, int pageSize = 10);
        CustomerDto GetCustomerWithMembershipTypeNBirthdate(int id);
    }
}