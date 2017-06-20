using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Persistence.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(DbContext context) : base(context)
        {
        }
        public VideoRentContext VideoRentContext => Context as VideoRentContext;


        public IList<Rental> GetAllRentalsWhithCustomersMoviesNMembershipType(int pageIndex, int pageSize = 10)
        {
            return VideoRentContext.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)                
                .OrderBy(r => r.Customer.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IList<Rental> GetAllRentalsWhithCustomersMoviesNMembershipType()
        {
            return VideoRentContext.Rentals
                 .Include(r => r.Customer)
                 .Include(r => r.Movie)
                 .OrderBy(r => r.Customer.Name)
                 .ToList();
        }
    }
}