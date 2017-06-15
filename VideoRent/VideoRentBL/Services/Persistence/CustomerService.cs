using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentBL.Services.Persistence
{
    public class CustomerService: Service<Customer,CustomerDto>,ICustomerService
    {
        private readonly IUnitOfWork _videoRent;
        public CustomerService(IUnitOfWork unitOfWork, IRepository<Customer> repository) : base(unitOfWork, repository)
        {
            _videoRent = unitOfWork;
        }
    }
}