using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Persistence.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(DbContext context) : base(context)
        {
        }
        public VideoRentContext VideoRentContext => Context as VideoRentContext;


        public IList<Rental> GetAllRentalsWhithCustomersMoviesNMembershipType(int pageIndex, int pageSize = 10)
        {
            return VideoRentContext.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)                
                .OrderBy(r => r.Customer.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IList<Rental> GetAllRentalsWhithCustomersMoviesNMembershipType(string search, int orderColm, string orderDir,
            out int totalRecords, out int recordSearched, int pageIndex, int pageSize = 10)
        {
            var data = VideoRentContext.Rentals.AsQueryable();
            totalRecords = data.Count();


            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(r => r.Customer.Name.ToUpper().Contains(search.ToUpper())
                                       ||
                                       r.Movie.Name.ToUpper().Contains(search.ToUpper())
                                       ||
                                       r.DateRented.ToString().ToUpper().Contains(search.ToUpper())
                                       ||
                                       r.DateReturned.Value.ToString().ToUpper().Contains(search.ToUpper())
                    );

                recordSearched = data.Count();
            }
            else
            recordSearched = totalRecords;

            var order = orderDir.ToUpper().Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                ? data
                    .Include(r => r.Customer)
                    .Include(r => r.Movie)
                    .OrderByDescending(OrderByList(orderColm))



                : data
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .OrderBy(OrderByList(orderColm));

           var result = order
               .Skip(pageIndex * pageSize)
               .Take(pageSize)
               .ToList();

            

            return result;
        }

        private static Func<Rental, string> OrderByList(int colmIdx)
        {
            
            switch (colmIdx)
            {
                case 0:
                    return r => r.Customer.Name;
                case 1:
                    return r => r.Movie.Name;
                case 3:
                    return r => r.DateRented.ToLongDateString();
                case 4:
                    return r => r.DateReturned.Value.ToLongDateString();
            }

            return r => r.Customer.Name;

        }

        public IList<Rental> GetAllRentalsWhithCustomersMoviesNMembershipType()
        {
            return VideoRentContext.Rentals
                 .Include(r => r.Customer)
                 .Include(r => r.Movie)
                 .OrderBy(r => r.Customer.Name)
                 .ToList();
        }
    }
}