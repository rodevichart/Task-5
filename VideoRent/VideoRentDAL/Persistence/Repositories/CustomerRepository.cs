using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Persistence.Repositories
{
    public class CustomerRepository:Repository<Customer>,ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }

        public VideoRentContext VideoRentContext => Context as VideoRentContext;
        public List<Customer> GetCustomersWithMembershipTypeNBirthdate(int pageIndex = 1, int pageSize = 10)
        {
            return VideoRentContext.Customers
                .Include(c => c.MembershipType)
                .OrderBy(c => c.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Customer GetCustomerWithMembershipTypeNBirthdate(int id)
        {
            return VideoRentContext.Customers.
                Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);
        }
    }
}