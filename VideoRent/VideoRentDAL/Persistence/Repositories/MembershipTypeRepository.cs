using System.Data.Entity;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Persistence.Repositories
{
    public class MembershipTypeRepository : Repository<MembershipType>, IMembershipTypeRepository
    {
        public MembershipTypeRepository(DbContext context) : base(context)
        {
        }

        public VideoRentContext CourseSaleContext => Context as VideoRentContext;
    }
}