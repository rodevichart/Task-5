using System.Collections.Generic;
using VideoRentDAL.Core.Domain;

namespace VideoRentDAL.Core.Repositories
{
    public interface IRentalRepository : IRepository<Rental>
    {
        IList<Rental> GetAllRentalsWhithCustomersMoviesNMembershipType(int pageIndex, int pageSize = 10);

        IList<Rental> GetAllRentalsWhithCustomersMoviesNMembershipType();
    }
}