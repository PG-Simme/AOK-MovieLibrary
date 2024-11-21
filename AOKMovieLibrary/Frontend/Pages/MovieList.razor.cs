namespace AOKMovieLibrary.Frontend.Pages;

public partial class MovieList
{
    [Inject] NavigationManager NavigationManager { get; set; } = null!;

    [Inject] MovieStateService MovieStateService { get; set; } = null!;

    [Inject] private IMovieService _movieService { get; set; } = null!;

    [Parameter] public List<MovieOverviewData> Movies { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Movies = (await _movieService.GetMoviesAsync()).Select(movie => movie.MapToMovieOverview()).ToList();
    }

    // Component specific filtering
    private MovieGenre SelectedGenreFilter { get; set; }

    private List<MovieOverviewData> FilteredMovies()
    {
        if (SelectedGenreFilter == MovieGenre.None)
        {
            return Movies;
        }

        return Movies
        .Where(m => m.Genre.HasFlag(SelectedGenreFilter))
        .ToList();
    }

    private void OnGenreFilterChanged(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty(e.Value?.ToString()))
        {
            SelectedGenreFilter = MovieGenre.None;
            return;
        }

        SelectedGenreFilter = Enum.Parse<MovieGenre>(e.Value.ToString());
    }

    // State service filtering

    //private List<MovieOverviewData> FilteredMovies() {
    //    if (MovieStateService.CurrentGenreFilter == MovieGenre.None)
    //    {
    //        return Movies;
    //    }

    //    return Movies
    //    .Where(m => m.Genre.HasFlag(MovieStateService.CurrentGenreFilter))
    //    .ToList();
    //}

    //private void OnGenreFilterChanged(ChangeEventArgs e)
    //{
    //    if (string.IsNullOrEmpty(e.Value?.ToString()))
    //    {
    //        MovieStateService.CurrentGenreFilter = MovieGenre.None;
    //        return;
    //    }

    //    MovieStateService.CurrentGenreFilter = Enum.Parse<MovieGenre>(e.Value.ToString());
    //}

    private void NavigateToDetails(MovieOverviewData movie)
    {
        // Navigate to the details page
        NavigationManager.NavigateTo($"/movie/{movie.Id}");
    }
}
