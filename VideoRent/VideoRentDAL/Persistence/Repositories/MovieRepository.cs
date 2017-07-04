using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VideoRentDAL.Core.Domain;
using VideoRentDAL.Core.Repositories;

namespace VideoRentDAL.Persistence.Repositories
{
    public class MovieRepository:Repository<Movie>,IMovieRepository
    {
        public MovieRepository(DbContext context) : base(context)
        {
        }

        public VideoRentContext VideoRentContext => Context as VideoRentContext;

        public IList<Movie> GetMovieWithGenre(string search, int orderColm, string orderDir, out int totalRecords,
            out int recordSearched, int pageIndex = 1, int pageSize = 10)
        {
            var data = VideoRentContext.Movies.AsQueryable();
            totalRecords = data.Count();


            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.Name.ToUpper().Contains(search.ToUpper())
                                       ||
                                       m.Genre.Name.ToUpper().Contains(search.ToUpper())
                    );

                recordSearched = data.Count();
            }
            else
                recordSearched = totalRecords;


            var result = orderDir.ToUpper().Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                ? data
                    .Include(m => m.Genre)
                    .OrderByDescending(OrderByList(orderColm))
                    .ToList()

                : data
                    .Include(m => m.Genre)
                    .OrderBy(OrderByList(orderColm))
                    .ToList();

            result = result
                .Skip(pageIndex*pageSize)
                .Take(pageSize).ToList();



            return result;
        }


        private static Func<Movie, string> OrderByList(int colmIdx)
        {
            if (colmIdx == 0)
                return (c => c.Name);
            return (c => c.Genre.Name);
        }

        public Movie GetCustomerWithMembershipTypeNBirthdate(int id)
        {
            return VideoRentContext.Movies
                .Include(m => m.Genre)
                .SingleOrDefault(c => c.Id == id); ;
        }
    }
}