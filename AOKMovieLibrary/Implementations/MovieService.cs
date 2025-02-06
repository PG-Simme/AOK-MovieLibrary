using Microsoft.EntityFrameworkCore;

namespace AOKMovieLibrary.Implementations;

public class MovieService : IMovieService
{
    private readonly IDbContextFactory<MovieContext> _contextFactory;
    private readonly MovieContext _context;

    public MovieService(IPersonService personService, IDbContextFactory<MovieContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _context = contextFactory.CreateDbContext();
    }

    public void SeedData(IEnumerable<Movie> movies)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Movies.AddRange(movies);
        context.SaveChanges();
    }

    public async Task<List<Movie>> GetMoviesAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Movies.ToListAsync();
    }

    public async Task<List<MovieOverviewData>> GetMoviesForOverviewAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        var movies = await context.Movies.Include(m => m.Director)
                                         .Include(m => m.Actors)
                                         .ToListAsync();

        return movies.Select(m => m.MapToMovieOverview()).ToList();
    }

    public async Task<Movie> GetMovieAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var movie = await context.Movies.Include(m => m.Director)
                                         .Include(m => m.Actors)
                                         .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            throw new InvalidOperationException($"Movie with id {id} not found");
        }

        return movie;
    }

    public async Task<MovieDetailData> GetMovieDetailsAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var movie = await context.Movies.Include(m => m.Director)
                                         .Include(m => m.Actors)
                                         .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            throw new InvalidOperationException($"Movie with id {id} not found");
        }

        return movie.MapToMovieDetails();
    }

    public async Task<Movie> CreateMovieAsync(CreateMovieCommand movie)
    {
        Movie newMovie = movie.MapToMovie();

        using var context = _contextFactory.CreateDbContext();

        var director = await context.Persons.FirstOrDefaultAsync(p => p.Id == movie.DirectorId);
        if (director == null)
        {
            throw new InvalidOperationException($"Director with id {movie.DirectorId} not found");
        }

        newMovie.Director = director;
        newMovie.DirectorId = director.Id;

        var actors = await context.Persons.Where(p => movie.Actors.Contains(p.Id)).ToListAsync();
        if (actors.Count != movie.Actors.Count)
        {
            throw new InvalidOperationException("One or more actors not found");
        }

        newMovie.Actors = actors;

        context.Movies.Add(newMovie);
        await context.SaveChangesAsync();
        return newMovie;
    }

    public async Task<MovieDetailData> UpdateMovieAsync(UpdateMovieCommand movie)
    {
        using var context = _contextFactory.CreateDbContext();
        using var transaction = await context.Database.BeginTransactionAsync();

        var existingMovie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);

        if (existingMovie == null)
        {
            throw new InvalidOperationException($"Movie with id {movie.Id} not found");
        }

        existingMovie.Title = movie.Title;
        existingMovie.Genre = movie.Genre;
        existingMovie.Year = movie.Year;
        existingMovie.Description = movie.Description;
        existingMovie.Runtime = movie.Runtime;

        context.Entry(existingMovie).Property(m => m.RowVersion).OriginalValue = movie.RowVersion;

        var director = await context.Persons.FirstOrDefaultAsync(p => p.Id == movie.DirectorId);
        if (director == null)
        {
            throw new InvalidOperationException($"Director with id {movie.DirectorId} not found");
        }

        existingMovie.DirectorId = movie.DirectorId;
        existingMovie.Actors = await context.Persons.Where(p => movie.Actors.Contains(p.Id)).ToListAsync();

        context.Update(existingMovie);
        await context.SaveChangesAsync();

        await transaction.CommitAsync();

        return existingMovie.MapToMovieDetails();
    }

    public async Task DeleteMovieAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        if (movie != null)
        {
            context.Movies.Remove(movie);
        }
    }
}
