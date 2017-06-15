using VideoRentDAL.Core;
using VideoRentDAL.Core.Repositories;
using VideoRentDAL.Persistence.Repositories;

namespace VideoRentDAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VideoRentContext _context;

        public UnitOfWork(VideoRentContext context)
        {
            _context = context;
            MoviesRepository = new MovieRepository(_context);
            CustomersRepository = new CustomerRepository(_context);
            MembershipTypeRepository = new MembershipTypeRepository(_context);
            RentalRepository = new RentalRepository(_context);
            GenreRepository = new GenreRepository(_context);
        }

        public IMovieRepository MoviesRepository { get; }
        public ICustomerRepository CustomersRepository { get; }
        public IMembershipTypeRepository MembershipTypeRepository { get; }
        public IRentalRepository RentalRepository { get; }
        public IGenreRepository GenreRepository { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}