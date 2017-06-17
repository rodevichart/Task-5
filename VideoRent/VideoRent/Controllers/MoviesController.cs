using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using VideoRent.Models;
using VideoRent.ViewModels;
using VideoRentBL.DTOs;
using VideoRentBL.Persistence;

namespace VideoRent.Controllers
{
    public class MoviesController : Controller
    {
        private VideoRentCore VideoRentCore { get; set; }

        public MoviesController()
        {
            VideoRentCore = new VideoRentCore();
        }

        public ActionResult New()
        {
            var genreListView = Mapper.Map<IEnumerable<GenreDto>,IEnumerable<Genre>>(VideoRentCore.UnitService.GenreService.GetAll()); 
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
            var movieList = VideoRentCore.UnitService.MovieService.GetCustomersWithMembershipTypeNBirthdate();
            var view = Mapper.Map<IList<MovieDto>, IList<Movie>>(movieList);
            return View(view);
        }

        //[Route("movies/details/{id:regex(\\d{4})}")]
        public ActionResult Details(int id)
        {
            var selected = VideoRentCore.UnitService.MovieService.GetCustomerWithMembershipTypeNBirthdate(id);
            var view = Mapper.Map<MovieDto, Movie>(selected);
            if (view == null)
                return HttpNotFound();
            return View(view);
        }

        public ActionResult Edit(int id)
        {
            var movie =
                Mapper.Map<MovieDto, Movie>(VideoRentCore.UnitService.MovieService.SingleOrDefault(c => c.Id == id));

            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres =  Mapper.Map<IEnumerable<GenreDto>, IEnumerable<Genre>>(VideoRentCore.UnitService.GenreService.GetAll())
            };
            return View("MovieForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var view = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = Mapper.Map<IEnumerable<GenreDto>, IEnumerable<Genre>>(VideoRentCore.UnitService.GenreService.GetAll())
                };
                return View("MovieForm", view);
            }
            if (movie.Id == null)
            {
                movie.DateAdded = DateTime.Now;
                VideoRentCore.UnitService.MovieService.Add(Mapper.Map<Movie, MovieDto>(movie));
            }
               
            else
            {
                var updateMovie = VideoRentCore.UnitService.MovieService.SingleOrDefault(m => m.Id == movie.Id);
                VideoRentCore.UnitService.MovieService.Update(Mapper.Map(movie, updateMovie));
            }
            return RedirectToAction("Index", "Movies");
        }
    }
}