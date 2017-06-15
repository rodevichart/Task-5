namespace VideoRentBL.Services.Core
{
    public interface IUnitOfWorkService
    {
        ICustomerService CustomerService { get; }
        IGenreService GenreService { get; }
        IMembershipTypeService MembershipTypeService { get; }
        IMovieService MovieService { get; }
        IRentalService RentalService {get;} 
    }
}