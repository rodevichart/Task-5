using VideoRentBL.Services.Core;
using VideoRentDAL.Core;

namespace VideoRentBL.Services.Persistence
{
    public class UnitOfWorkService:IUnitOfWorkService
    {
        public UnitOfWorkService(IUnitOfWork unitOfWork)
        {
            CustomerService = new CustomerService(unitOfWork, unitOfWork.CustomersRepository);
            GenreService = new GenreService(unitOfWork, unitOfWork.GenreRepository);
            MembershipTypeService = new MembershipTypeService(unitOfWork, unitOfWork.MembershipTypeRepository);
            MovieService = new MovieService(unitOfWork, unitOfWork.MoviesRepository);
            RentalService = new RentalService(unitOfWork, unitOfWork.RentalRepository);
        }
        public ICustomerService CustomerService { get; }
        public IGenreService GenreService { get; }
        public IMembershipTypeService MembershipTypeService { get; }
        public IMovieService MovieService { get; }
        public IRentalService RentalService { get; }
    }
}