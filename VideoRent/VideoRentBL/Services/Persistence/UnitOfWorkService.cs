using System;
using VideoRentBL.Services.Core;
using VideoRentDAL.Core;

namespace VideoRentBL.Services.Persistence
{
    public class UnitOfWorkService:IUnitOfWorkService, IDisposable
    {
        private ICustomerService _customerService;

        private IGenreService _genreService;

        private IMembershipTypeService _membershipTypeService;

        private IMovieService _movieService;

        private IRentalService _rentalService;

        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public ICustomerService CustomerService => _customerService ??
                                                   (_customerService = new CustomerService(_unitOfWork, _unitOfWork.CustomersRepository));

        public IGenreService GenreService => _genreService ??
                                                   (_genreService = new GenreService(_unitOfWork, _unitOfWork.GenreRepository));

        public IMembershipTypeService MembershipTypeService => _membershipTypeService ??
                                                  (_membershipTypeService = new MembershipTypeService(_unitOfWork, _unitOfWork.MembershipTypeRepository));

        public IMovieService MovieService => _movieService ??
                                                  (_movieService = new MovieService(_unitOfWork, _unitOfWork.MoviesRepository));

        public IRentalService RentalService => _rentalService ??
                                                  (_rentalService = new RentalService(_unitOfWork, _unitOfWork.RentalRepository));

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}