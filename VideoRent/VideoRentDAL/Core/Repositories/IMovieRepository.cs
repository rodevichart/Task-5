using System.Collections.Generic;
using VideoRentDAL.Core.Domain;

namespace VideoRentDAL.Core.Repositories
{
    public interface IMovieRepository:IRepository<Movie>
    {      
        IList<Movie> GetMovieWithGenre(string search, int orderColm, string orderDir, out int totalRecords,
           out int recordSearched, int pageIndex = 1, int pageSize = 10);
        Movie GetCustomerWithMembershipTypeNBirthdate(int id);
    }
}