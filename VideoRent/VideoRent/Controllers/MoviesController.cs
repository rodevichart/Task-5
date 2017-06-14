using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoRent.Models;

namespace VideoRent.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
       // [Route("movies")]
        public ActionResult Index()
        {
            var movieList = new List<Movie>
            {
                new Movie {Id = 1,Name = "Red Moon"},
                new Movie {Id = 2,Name = "White Power"}
            };          

            return View(movieList);
        }

        //[Route("movies/details/{id:regex(\\d{4})}")]
        public ActionResult Details(int? id)
        {
            var movieList = new List<Movie>
            {
                new Movie {Id = 1,Name = "Red Moon"},
                new Movie {Id = 2,Name = "White Power"}
            };

            var selected = movieList.SingleOrDefault(m => m.Id == id);
            if (selected == null)
                return HttpNotFound();
            return View(selected);
        }
    }
}