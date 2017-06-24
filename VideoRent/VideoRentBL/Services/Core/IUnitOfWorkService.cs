using System;

namespace VideoRentBL.Services.Core
{
    public interface IUnitOfWorkService: IDisposable
    {
        ICustomerService CustomerService { get; }
        IGenreService GenreService { get; }
        IMembershipTypeService MembershipTypeService { get; }
        IMovieService MovieService { get; }
        IRentalService RentalService {get;} 
    }
}