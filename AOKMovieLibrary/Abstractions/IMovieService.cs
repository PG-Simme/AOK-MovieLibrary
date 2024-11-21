namespace AOKMovieLibrary.Abstractions;

public interface IMovieService
{
    void SeedData(IEnumerable<Movie> movies);

    Task<List<Movie>> GetMoviesAsync();
    
    Task<List<MovieOverviewData>> GetMoviesForOverviewAsync();

    Task<Movie> GetMovieAsync(int id);

    Task<MovieDetailData> GetMovieDetailsAsync(int id);

    Task<Movie> CreateMovieAsync(CreateMovieCommand movie);

    Task<Movie> UpdateMovieAsync(Movie movie);

    Task DeleteMovieAsync(int id);
}
