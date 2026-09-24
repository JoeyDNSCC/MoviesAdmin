using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MoviesAdmin.Models;
using System.Diagnostics;

namespace MoviesAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetMovie()
        {
            Movie movie = new Movie();
            movie.Id = 1;
            movie.Title = "Grand Budapest Hotel";
            movie.Description = "idk man I fell asleep";
            movie.Rating = "PG-13";
            movie.Genre = "idk man";
            movie.Runtime = 224;
            return View(movie);
        }

        public IActionResult AllMovies()
        {
            List<Movie> movies = new List<Movie>();

            Movie movie1 = new Movie();
            movie1.Id = 1;
            movie1.Title = "Oceans 11";
            movie1.Description = "George Clooney robs a casino";
            movie1.Rating = "PG-13";
            movie1.Genre = "Heist";
            movie1.Runtime = 110;
            movie1.YearReleased = 2012;

            Movie movie2 = new Movie();
            movie2.Id = 2;
            movie2.Title = "Grand Budapest Hotel";
            movie2.Description = "people in cool outfits run a hotel";
            movie2.Rating = "PG-13";
            movie2.Genre = "Drama";
            movie2.Runtime = 124;
            movie2.YearReleased = 2019;

            Movie movie3 = new Movie();
            movie3.Id = 3;
            movie3.Title = "Conan the Barbarian";
            movie3.Description = "Arnold Schwartznegger punches a llama then slaughters everyone at a snake orgy";
            movie3.Rating = "R";
            movie3.Genre = "Fantasy";
            movie3.Runtime = 143;
            movie3.YearReleased = 1984;

            movies.Add(movie1);
            movies.Add(movie2);
            movies.Add(movie3);

            return View(movies);
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
