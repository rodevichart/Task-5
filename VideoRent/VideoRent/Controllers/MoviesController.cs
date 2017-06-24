using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using VideoRent.Models;
using VideoRent.Models.JsonDatatables;
using VideoRent.ViewModels;
using VideoRentBL.DTOs;
using VideoRentBL.Services.Core;

namespace VideoRent.Controllers
{
    public class MoviesController : AbastractController
    {
        public MoviesController(IUnitOfWorkService logic) : base(logic)
        {
        }

        [Route("GetMovies")]
        public ActionResult GetMovies(string query = null)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                var movies = Mapper.Map<IEnumerable<MovieDto>, IEnumerable<Movie>>
                    (Logic.MovieService.Find(c => c.Name.Contains(query) && c.NumberAvailable > 0));

                var json = (new CamelCaseResolver
                {
                    Data = movies,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                });
                return json;
            }
            return HttpNotFound("Movie not founded");
        }


        [Authorize(Roles = RoleName.CanManageMoviesCustomers)]
        public ActionResult New()
        {
            var genreListView = Mapper.Map<IEnumerable<GenreDto>,IEnumerable<Genre>>(Logic.GenreService.GetAll()); 
            var viewModel = new MovieFormViewModel
            {
                Genres = genreListView,
                Movie = new Movie()
            };

            return View("MovieForm",viewModel);
        }

        // GET: Movies
        // [Route("movies")]
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMoviesCustomers))
                return View("List");
            return View("ReadOnlyList");
        }

        [HttpPost]
        public ActionResult Index(DataTableAjaxPostModel model)
        {
            if (model == null)
                return HttpNotFound();

            int totalRecords;
            int recordsSearched;

            var orderColm = model.order.ElementAtOrDefault(0)?.column ?? 0;
            var orderDir = model.order?.ElementAtOrDefault(0)?.dir;
            var start = model.start.HasValue ? model.start / 10 : 0;

            var movieList =
                Logic.MovieService.GetMovieWithGenre(model.search.value, orderColm, orderDir,
                    out totalRecords, out recordsSearched, start.Value, model.length ?? 10);
            var view = Mapper.Map<IList<MovieDto>, IList<Movie>>(movieList);

            var json = (new CamelCaseResolver
            {
                Data = new { draw = model.draw, recordsFiltered = recordsSearched, recordsTotal = totalRecords, data = view },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            });
            return json;
        }

        //[Route("movies/details/{id:regex(\\d{4})}")]
        public ActionResult Details(int id)
        {
            var selected = Logic.MovieService.GetCustomerWithMembershipTypeNBirthdate(id);
            var view = Mapper.Map<MovieDto, Movie>(selected);
            if (view == null)
                return HttpNotFound();
            return View(view);
        }

        public ActionResult Edit(int id)
        {
            var movie =
                Mapper.Map<MovieDto, Movie>(Logic.MovieService.SingleOrDefault(c => c.Id == id));

            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres =  Mapper.Map<IEnumerable<GenreDto>, IEnumerable<Genre>>(Logic.GenreService.GetAll())
            };
            return View("MovieForm",viewModel);
        }


        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMoviesCustomers)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var view = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = Mapper.Map<IEnumerable<GenreDto>, IEnumerable<Genre>>(Logic.GenreService.GetAll())
                };
                return View("MovieForm", view);
            }
            if (movie.Id == null)
            {
                movie.DateAdded = DateTime.Now;
                Logic.MovieService.Add(Mapper.Map<Movie, MovieDto>(movie));
            }
               
            else
            {
                Logic.MovieService.Update(Mapper.Map<Movie, MovieDto>(movie));
            }
            return RedirectToAction("Index", "Movies");
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMoviesCustomers)]
        public HttpStatusCode Delete(int id)
        {
            var customer = Logic.MovieService.SingleOrDefault(c => c.Id == id);
            if (customer != null)
            {
                Logic.MovieService.Remove(customer.Id);
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }
    }
}