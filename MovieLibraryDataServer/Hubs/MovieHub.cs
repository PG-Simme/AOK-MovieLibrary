using Microsoft.AspNetCore.SignalR;

namespace MovieLibraryDataServer.Hubs;

public class MovieHub : Hub
{
    private readonly IMovieService _movieService;

    public MovieHub(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task GetMovies()
    {
        await Clients.All.SendAsync("ReceiveMovies", await _movieService.GetMoviesForOverviewAsync());
    }

    public async Task GetMovieDetails(int id)
    {
        await Clients.All.SendAsync("ReceiveMovieDetails", await _movieService.GetMovieDetailsAsync(id));
    }

    public async Task CreateMovie(CreateMovieCommand createMovieCommand)
    {
        await Clients.Caller.SendAsync("ReceiveCreateMovieResult", await _movieService.CreateMovieAsync(createMovieCommand));
    }

    public async Task UpdateMovie(UpdateMovieCommand updateMovieCommand)
    {
        await Clients.Caller.SendAsync("ReceiveUpdateMovieResult", await _movieService.UpdateMovieAsync(updateMovieCommand));
    }

    public async Task DeleteMovie(int id)
    {
        await Clients.Caller.SendAsync("ReceiveDeleteMovieResult", _movieService.DeleteMovieAsync(id));
    }
}
