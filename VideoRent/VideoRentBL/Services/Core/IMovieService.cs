using System.Collections.Generic;
using VideoRentBL.DTOs;
using VideoRentDAL.Core.Domain;

namespace VideoRentBL.Services.Core
{
    public interface IMovieService:IService<Movie,MovieDto>
    {
        IList<MovieDto> GetCustomersWithMembershipTypeNBirthdate(int pageIndex = 1, int pageSize = 10);
        MovieDto GetCustomerWithMembershipTypeNBirthdate(int id);

    }
}