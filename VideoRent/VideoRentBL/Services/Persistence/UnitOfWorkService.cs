using VideoRentBL.Services.Core;
using VideoRentDAL.Core;
using VideoRentDAL.Persistence;

namespace VideoRentBL.Services.Persistence
{
    public class UnitOfWorkService:IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CustomerService = new CustomerService(_unitOfWork,_unitOfWork.CustomersRepository);
            GenreService = new GenreService(_unitOfWork,_unitOfWork.GenreRepository);
            MembershipTypeService = new MembershipTypeService(_unitOfWork,_unitOfWork.MembershipTypeRepository);
            MovieService = new MovieService(_unitOfWork,_unitOfWork.MoviesRepository);
            RentalService = new RentalService(_unitOfWork,_unitOfWork.RentalRepository);
        }
        public ICustomerService CustomerService { get; }
        public IGenreService GenreService { get; }
        public IMembershipTypeService MembershipTypeService { get; }
        public IMovieService MovieService { get; }
        public IRentalService RentalService { get; }
    }
}