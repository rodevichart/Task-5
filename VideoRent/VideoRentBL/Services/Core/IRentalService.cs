using System.Collections.Generic;
using VideoRentBL.DTOs;
using VideoRentBL.Lib;

namespace VideoRentBL.Services.Core
{
    public interface IRentalService : IService<RentalDto>
    {
        IList<RentalDto> GetAllRentalsWhithCustomersMoviesNMembershipType(int pageIndex, int pageSize = 10);

        IList<ChartDetails> GetCountRentalsMovies();
    }
}