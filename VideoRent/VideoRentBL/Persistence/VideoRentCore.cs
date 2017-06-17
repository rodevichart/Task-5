using AutoMapper;
using VideoRentBL.Services.Core;
using VideoRentBL.Services.Persistence;
using VideoRentDAL.Core;
using VideoRentDAL.Persistence;

namespace VideoRentBL.Persistence
{
    public class VideoRentCore
    {
        public IUnitOfWorkService UnitService { get; set; }

        public VideoRentCore()
        {
            //Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            IUnitOfWork unitOfWork = new UnitOfWork(new VideoRentContext());
            UnitService = new UnitOfWorkService(unitOfWork);
        }
    }
}