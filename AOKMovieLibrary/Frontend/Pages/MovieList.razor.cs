namespace AOKMovieLibrary.Frontend.Pages;

public partial class MovieList
{
    [Inject] private IMovieService _movieService { get; set; } = null!;

    [Parameter] public List<MovieOverviewData> Movies { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Movies = (await _movieService.GetMoviesAsync()).Select(movie => movie.MapToMovieOverview()).ToList();
    }

    private void NavigateToDetails(MovieOverviewData movie)
    {
        // Navigate to the details page
        Console.WriteLine($"Navigating to details for movie {movie.Title}");
    }
}
