using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }

        public VideoRentContext VideoRentContext => Context as VideoRentContext;

        public IList<Customer> GetCustomersWithMembershipTypeNBirthdate(string search, int orderColm, string orderDir,
            out int totalRecords,
            out int recordSearched, int pageIndex = 1, int pageSize = 10)
        {
            var data = VideoRentContext.Customers.AsQueryable();
            totalRecords = data.Count();


            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(c => c.Name.ToUpper().Contains(search.ToUpper())
                                       ||
                                       c.MembershipType.Name.ToUpper().Contains(search.ToUpper())
                    );
                recordSearched = data.Count();
            }
            else
                recordSearched = totalRecords;



            var order = orderDir.ToUpper().Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                ? data
                    .Include(c => c.MembershipType)
                    .OrderByDescending(OrderByList(orderColm))
                : data
                    .Include(c => c.MembershipType)
                    .OrderBy(OrderByList(orderColm));
    

             var result = order
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }

        public Customer GetCustomerWithMembershipTypeNBirthdate(int id)
        {
            return VideoRentContext.Customers.
                Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);
        }

        private static Expression<Func<Customer, string>> OrderByList(int colmIdx)
        {
            if (colmIdx == 0)
                return (c => c.Name);
            return (c => c.MembershipType.Name);
        }


    }
}