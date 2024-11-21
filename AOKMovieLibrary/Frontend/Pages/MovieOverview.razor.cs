namespace AOKMovieLibrary.Frontend.Pages;

public partial class MovieOverview
{
    [Inject] NavigationManager NavigationManager { get; set; } = null!;

    [Inject] MovieStateService MovieStateService { get; set; } = null!;

    [Inject] ProtectedLocalStorage ProtectedLocalStorage { get; set; } = null!;

    [Inject] ProtectedSessionStorage ProtectedSessionStorage { get; set; } = null!;

    private MovieGenre SelectedGenreFilter { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // State service filtering

        //SelectedGenreFilter = MovieStateService.CurrentGenreFilter;

        // use local storage if prefered
        //var genre = await ProtectedSessionStorage.GetAsync<MovieGenre>(nameof(MovieGenre));

        //if (!genre.Success || genre.Value == MovieGenre.None)
        //{
        //    SelectedGenreFilter = MovieGenre.None;
        //}

        //SelectedGenreFilter = genre.Value;
    }

    // Component specific filtering

    private void OnGenreFilterChanged(MovieGenre genre)
    {
        SelectedGenreFilter = genre;
    }

    // State service filtering

    //private void OnGenreFilterChanged(MovieGenre genre)
    //{
    //    MovieStateService.CurrentGenreFilter = genre;
    //    SelectedGenreFilter = genre;
    //}

    // Storage/Session specific filtering

    //private async Task OnGenreFilterChanged(MovieGenre genre)
    //{
    //    // use local storage if prefered
    //    await ProtectedSessionStorage.SetAsync(nameof(MovieGenre), genre);
    //    SelectedGenreFilter = genre;
    //}

    private void NavigateToAddMovie()
    {
        NavigationManager.NavigateTo("/movie/add");
    }
}
