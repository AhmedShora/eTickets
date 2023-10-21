using eTickets.Data.ViewModels;
using System.Transactions;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepo<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;

        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddMovieAsync(MovieVM movie)
        {
            var data = new Movie()
            {
                Title = movie.Title,
                ImageUrl = movie.ImageUrl,
                Description = movie.Description,
                EndDate = movie.EndDate,
                StartDate = movie.StartDate,
                Price = movie.Price,
                ProducerId = movie.ProducerId,
                MovieCategory = movie.MovieCategory,
                CinemaId = movie.CinemaId
            };
            await _context.Movies.AddAsync(data);
            await _context.SaveChangesAsync();

            //add movie actor
            foreach (var actorId in movie.ActorIds)
            {
                var NewActorMovie = new ActorMovie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.ActorMovies.AddAsync(NewActorMovie);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies.Include(a => a.Cinema).Include(b => b.Producer)
                 .Include(c => c.ActorMovies).ThenInclude(d => d.Actor).SingleOrDefaultAsync(v => v.Id == id);
            return movie;
        }

        public async Task<MoviesDropDownVM> MovieDropDowns()
        {
            var response = new MoviesDropDownVM()
            {
                // response.Actors = await _context.Actors.Select(a =>new { a.Id,a.FullName }).OrderBy(aa => aa.FullName).ToListAsync();
                Actors = await _context.Actors.OrderBy(aa => aa.FullName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(aa => aa.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(aa => aa.Name).ToListAsync()
            };
            return response;
        }

        public async Task UpdateMovieAsync(MovieVM movie)
        {
            //try to make it transaction
            var movieData = await _context.Movies.FirstOrDefaultAsync(aa => aa.Id == movie.Id);

            if (movieData != null)
            {
                movieData.Title = movie.Title;
                movieData.ImageUrl = movie.ImageUrl;
                movieData.Description = movie.Description;
                movieData.EndDate = movie.EndDate;
                movieData.StartDate = movie.StartDate;
                movieData.Price = movie.Price;
                movieData.ProducerId = movie.ProducerId;
                movieData.MovieCategory = movie.MovieCategory;
                movieData.CinemaId = movie.CinemaId;
                await _context.SaveChangesAsync();
            }
            //remove existing actors
            List<ActorMovie> actorsInMovie = _context.ActorMovies.Where(aa => aa.MovieId == movieData.Id).ToList();
            _context.ActorMovies.RemoveRange(actorsInMovie);
            await _context.SaveChangesAsync();

            //add movie actor
            foreach (var actorId in movie.ActorIds)
            {
                var NewActorMovie = new ActorMovie()
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };
                await _context.ActorMovies.AddAsync(NewActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
