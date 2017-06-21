using System.Collections.Generic;
using VideoRentBL.DTOs;

namespace VideoRentBL.Services.Core
{
    public interface IMovieService:IService<MovieDto>
    {
        IList<MovieDto> GetMovieWithGenre(string search, int orderColm, string orderDir, out int totalRecords,
           out int recordSearched, int pageIndex = 1, int pageSize = 10);
        MovieDto GetCustomerWithMembershipTypeNBirthdate(int id);

    }
}