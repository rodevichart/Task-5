using System.Web.Mvc;
using VideoRentBL.Services.Core;

namespace VideoRent.Controllers
{
    public abstract class AbastractController : Controller
    {
        protected IUnitOfWorkService Logic { get; }

        protected AbastractController(IUnitOfWorkService logic)
        {
            Logic = logic;
        }
    }
}