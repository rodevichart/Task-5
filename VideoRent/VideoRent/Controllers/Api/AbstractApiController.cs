using System.Web.Http;
using VideoRentBL.Services.Core;

namespace VideoRent.Controllers.Api
{
    public class AbstractApiController:ApiController
    {
        protected IUnitOfWorkService Logic { get; }

        public AbstractApiController()
        {           
        }

        protected AbstractApiController(IUnitOfWorkService logic)
        {
            Logic = logic;
        }


    }
    

}