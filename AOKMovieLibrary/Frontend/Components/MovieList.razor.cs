namespace AOKMovieLibrary.Frontend.Components;

public partial class MovieList
{
    [Inject] NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IMovieService _movieService { get; set; } = null!;

    [CascadingParameter] public MovieGenre SelectedGenreFilter { get; set; }

    [Parameter] public List<MovieOverviewData> Movies { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Movies = (await _movieService.GetMoviesAsync()).Select(movie => movie.MapToMovieOverview()).ToList();
    }

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

    private void NavigateToDetails(MovieOverviewData movie)
    {
        // Navigate to the details page
        NavigationManager.NavigateTo($"/movie/{movie.Id}");
    }
}
