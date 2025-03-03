using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

namespace AOKMovieLibrary.Implementations;

public class MovieService : IMovieService
{
    private readonly IDbContextFactory<MovieContext> _contextFactory;
    private readonly MovieContext _context;
    private HubConnection _hubConnection;

    public MovieService(IPersonService personService, IDbContextFactory<MovieContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _context = contextFactory.CreateDbContext();

        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7108/moviehub")
            .Build();
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
        List<MovieOverviewData> movies = [];

        _hubConnection.On<List<MovieOverviewData>>("ReceiveMovies", (data) =>
        {
            movies = data;
        });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("GetMovies");

        return movies;
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
        MovieDetailData movie = new();

        _hubConnection.On<MovieDetailData>("ReceiveMovieDetails", (data) =>
        {
            movie = data;
        });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("GetMovieDetails", id);

        return movie;
    }

    public async Task CreateMovieAsync(CreateMovieCommand movie)
    {
        _hubConnection.On<Movie>("ReceiveCreateMovieResult", (data) => { });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("CreateMovie", movie);
    }

    public async Task<MovieDetailData> UpdateMovieAsync(UpdateMovieCommand updateMovieCommand)
    {
        MovieDetailData movie = new();

        _hubConnection.On<MovieDetailData>("ReceiveUpdateMovieResult", (data) => movie = data);

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("UpdateMovie", updateMovieCommand);

        return movie;
    }

    public async Task DeleteMovieAsync(int id)
    {
        _hubConnection.On("ReceiveDeleteMovieResult", () => { });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("DeleteMovie", id);
    }
}
