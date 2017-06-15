using System;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Core
{
    public interface IUnitOfWork:IDisposable
    {
        IMovieRepository MoviesRepository { get; }
        ICustomerRepository CustomersRepository { get; }
        IMembershipTypeRepository MembershipTypeRepository { get; }
        IRentalRepository RentalRepository { get; }
        IGenreRepository GenreRepository { get; }
        int Complete();
    }
}