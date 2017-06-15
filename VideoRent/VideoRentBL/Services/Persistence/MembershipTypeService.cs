using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentBL.Services.Persistence
{
    public class MembershipTypeService:Service<MembershipType,MembershipTypeDto>,IMembershipTypeService
    {
        public MembershipTypeService(IUnitOfWork unitOfWork, IRepository<MembershipType> repository) : base(unitOfWork, repository)
        {
        }
    }
}