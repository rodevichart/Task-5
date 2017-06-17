using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VideoRentBL;
using VideoRentBL.DTOs;
using VideoRentBL.Persistence;
using VideoRentBL.Services.Core;
using VideoRentBL.Services.Persistence;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Persistence;

namespace TestConsole
{
    class Program
    {
        public static IUnitOfWorkService UnitService { get; set; }
        private static IUnitOfWork _unitOfWork;
        static void Main(string[] args)
        {
            //Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            Mapper.Initialize(c => c.AddProfile<MappingProfileMVC>());
            var videoRentContext = new VideoRentContext(); 
            _unitOfWork = new UnitOfWork(videoRentContext);
            UnitService = new UnitOfWorkService(_unitOfWork);
           //var viderentcore = new VideoRentCore();
            /*var newRent = new RentalDto {CustomerId = 4,MovieId = 3,DateRented = DateTime.Now};

            var allRentals = UnitService.RentalService.GetAllRentalsWhithCustomersMoviesNMembershipType(1);
            foreach (var rental in allRentals)
            {
                Console.WriteLine(rental.Movie.Name);
                Console.WriteLine(rental.Customer.Name);
                
            }*/

            Console.WriteLine("---------------------");
            var allCustomersDto = UnitService.CustomerService.GetCustomersWithMembershipTypeNBirthdate();
            var allCustomers = Mapper.Map<IList<CustomerDto>, IList<Customer>>(allCustomersDto);

            foreach (var allCustomer in allCustomers)
            {
                Console.WriteLine(allCustomer.Name);
            }
            //UnitService.RentalService.Add(newRent);
            Console.ReadLine();
        }
    }
}
