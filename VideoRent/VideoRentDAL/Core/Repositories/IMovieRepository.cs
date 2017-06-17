using System.Collections.Generic;
using VideoRentDAL.Core.Domain;

namespace VideoRentDAL.Core.Repositories
{
    public interface IMovieRepository:IRepository<Movie>
    {
        List<Movie> GetCustomersWithMembershipTypeNBirthdate(int pageIndex = 1, int pageSize = 10);
        Movie GetCustomerWithMembershipTypeNBirthdate(int id);
    }
}