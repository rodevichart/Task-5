using System.Collections.Generic;
using VideoRentBL.DTOs;

namespace VideoRentBL.Services.Core
{
    public interface IMovieService:IService<MovieDto>
    {
        IList<MovieDto> GetCustomersWithMembershipTypeNBirthdate(int pageIndex = 1, int pageSize = 10);
        MovieDto GetCustomerWithMembershipTypeNBirthdate(int id);

    }
}