using eTickets.Data.ViewModels;

namespace eTickets.Data.Services
{
    public interface IMoviesService : IEntityBaseRepo<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<MoviesDropDownVM> MovieDropDowns();
        Task AddMovieAsync(MovieVM movie);
        Task UpdateMovieAsync(MovieVM movie);
    }
}
