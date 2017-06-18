using Ninject.Modules;
using VideoRentDAL.Core;
using VideoRentDAL.Persistence;

namespace VideoRentBL
{
    public class NinjectBLModule : NinjectModule
    {
        private readonly string _connectionString;


        public NinjectBLModule(string connection)
        {
            _connectionString = connection;
        }


        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_connectionString);
        }
    }
}