namespace AOKMovieLibrary.Frontend.Pages;

public partial class MovieDetails
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IMovieService _movieService { get; set; } = null!;

    [Parameter]
    public int Id { get; set; }

    private MovieDetailData? _movieDetails;

    protected override async Task OnInitializedAsync()
    {
        _movieDetails = (await _movieService.GetMovieAsync(Id)).MapToMovieDetails();
    }

    private void GoBack()
    {
        NavigationManager.NavigateTo("/movies");
    }
}
