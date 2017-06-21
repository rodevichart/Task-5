using System.Collections.Generic;
using VideoRentBL.DTOs;
using VideoRentBL.Lib;

namespace VideoRentBL.Services.Core
{
    public interface IRentalService : IService<RentalDto>
    {
        IList<RentalDto> GetAllRentalsWhithCustomersMoviesNMembershipType(string search, int orderColm, string orderDir, out int totalRecords,
            out int recordSearched, int pageIndex = 1, int pageSize = 10);

        IList<ChartDetails> GetCountRentalsMovies();
    }
}