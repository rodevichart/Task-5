using System.Data.Entity;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Persistence.Repositories
{
    public class CustomerRepository:Repository<Customer>,ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }

        public VideoRentContext CourseSaleContext => Context as VideoRentContext;
    }
}