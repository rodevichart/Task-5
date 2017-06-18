using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using VideoRent.Models;
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
            var movieList = Logic.MovieService.GetCustomersWithMembershipTypeNBirthdate();
            var view = Mapper.Map<IList<MovieDto>, IList<Movie>>(movieList);
            return View(view);
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
    }
}