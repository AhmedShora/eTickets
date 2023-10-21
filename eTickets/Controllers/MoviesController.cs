using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            //var movies=await _context.Movies.Include(c=>c.Cinema).OrderBy(a=>a.Title).ToListAsync();
            var movies = await _service.GetAllAsync(a => a.Cinema);
            return View(movies);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var movies = await _service.GetAllAsync(a => a.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filtered = movies.Where(a => a.Description.ToLower().Contains(searchString.ToLower()) ||
                     a.Title.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filtered);
            }
            return View("Index", movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            return View(movie);

        }

        public async Task<IActionResult> Create()
        {
            var lists = await _service.MovieDropDowns();
            ViewBag.Cinemas = new SelectList(lists.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(lists.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(lists.Producers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var lists = await _service.MovieDropDowns();
                ViewBag.Cinemas = new SelectList(lists.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(lists.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(lists.Producers, "Id", "FullName");
                return View(movie);

            }
            if (movie.StartDate > movie.EndDate)
            {
                //it does not been shown in model state error
                //to do
                ModelState.AddModelError("Not Normal", "End date must be after start date");

                var lists = await _service.MovieDropDowns();
                ViewBag.Cinemas = new SelectList(lists.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(lists.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(lists.Producers, "Id", "FullName");
                return View(movie);
            }
            await _service.AddMovieAsync(movie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if (movie == null) return View("NotFound");
            var movieDetails = new MovieVM()
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl,
                Price = movie.Price,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId,
                ActorIds = movie.ActorMovies.Select(n => n.ActorId).ToList()
            };
            var lists = await _service.MovieDropDowns();
            ViewBag.Cinemas = new SelectList(lists.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(lists.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(lists.Producers, "Id", "FullName");
            return View(movieDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var lists = await _service.MovieDropDowns();
                ViewBag.Cinemas = new SelectList(lists.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(lists.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(lists.Producers, "Id", "FullName");
                return View(movie);

            }
            if (movie.StartDate > movie.EndDate)
            {
                ModelState.AddModelError("Not Normal", "End date must be after start date");

                var lists = await _service.MovieDropDowns();
                ViewBag.Cinemas = new SelectList(lists.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(lists.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(lists.Producers, "Id", "FullName");
                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction("Index");
        }

    }
}
