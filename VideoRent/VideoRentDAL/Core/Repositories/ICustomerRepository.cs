using System.Collections.Generic;
using VideoRentDAL.Core.Domain;

namespace VideoRentDAL.Core.Repositories
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        //List<Customer> GetCustomersWithMembershipType();
        List<Customer> GetCustomersWithMembershipTypeNBirthdate(string search, int? orderColm, string orderDir, out int totalRecords,
            out int recordSearched, int pageIndex = 1, int pageSize = 10);
        Customer GetCustomerWithMembershipTypeNBirthdate(int id);

    }
}