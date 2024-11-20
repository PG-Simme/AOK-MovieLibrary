namespace AOKMovieLibrary.Implementations;

public class MovieService : IMovieService
{
    private List<Movie> _movies = [];

    public void SeedData(IEnumerable<Movie> movies)
    {
        _movies = movies.ToList();
    }

    public async Task<List<Movie>> GetMoviesAsync()
    {
        return _movies;
    }

    public async Task<List<MovieOverviewData>> GetMoviesForOverviewAsync()
    {
        return _movies.Select(m => m.MapToMovieOverview()).ToList();
    }

    public async Task<Movie> GetMovieAsync(int id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);

        if (movie == null)
        {
            throw new InvalidOperationException($"Movie with id {id} not found");
        }

        return movie;
    }

    public async Task<MovieDetailData> GetMovieDetailsAsync(int id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);

        if (movie == null)
        {
            throw new InvalidOperationException($"Movie with id {id} not found");
        }

        return movie.MapToMovieDetails();
    }

    public async Task<Movie> CreateMovieAsync(Movie movie)
    {
        if (_movies.Count == 0)
        {
            movie.Id = 0;
        }
        else
        {
            movie.Id = _movies.Max(m => m.Id) + 1;
        }

        _movies.Add(movie);
        return movie;
    }

    public async Task<Movie> UpdateMovieAsync(Movie movie)
    {
        var existingMovie = _movies.FirstOrDefault(m => m.Id == movie.Id);

        if (existingMovie == null)
        {
            throw new InvalidOperationException($"Movie with id {movie.Id} not found");
        }

        existingMovie.Title = movie.Title;
        existingMovie.Genre = movie.Genre;
        existingMovie.Year = movie.Year;
        existingMovie.Director = movie.Director;
        existingMovie.Actors = movie.Actors;
        existingMovie.Description = movie.Description;
        existingMovie.Runtime = movie.Runtime;

        return existingMovie;
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);
        if (movie != null)
        {
            _movies.Remove(movie);
        }
    }
}
