using MovieLibraryDataServer.Models.Commands;
using MovieLibraryDataServer.Models.DAL;
using MovieLibraryDataServer.Models.ViewModels;

namespace MovieLibraryDataServer.Abstractions;

public interface IMovieService
{
    void SeedData(IEnumerable<Movie> movies);

    Task<List<Movie>> GetMoviesAsync();

    Task<List<MovieOverviewData>> GetMoviesForOverviewAsync();

    Task<Movie> GetMovieAsync(int id);

    Task<MovieDetailData> GetMovieDetailsAsync(int id);

    Task<Movie> CreateMovieAsync(CreateMovieCommand movie);

    Task<MovieDetailData> UpdateMovieAsync(UpdateMovieCommand movie);

    Task DeleteMovieAsync(int id);
}
