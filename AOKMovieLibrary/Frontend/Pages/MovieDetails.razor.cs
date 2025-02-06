namespace AOKMovieLibrary.Frontend.Pages;

public partial class MovieDetails
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IMovieService _movieService { get; set; } = null!;

    [Inject] private IPersonService _personService { get; set; } = null!;

    [Parameter]
    public int Id { get; set; }

    private UpdateMovieCommand _movieDetails;
    private IEnumerable<string> GenreOptions => Enum.GetNames<MovieGenre>();
    private IEnumerable<PersonMetaData> AvailableDirectors = [];
    
    private string SelectedGenre
    {
        get => _movieDetails.Genre.ToString();
        set => _movieDetails.Genre = Enum.Parse<MovieGenre>(value);
    }

    protected override async Task OnInitializedAsync()
    {
        MovieDetailData detailData = await _movieService.GetMovieDetailsAsync(Id);
        _movieDetails = new UpdateMovieCommand
        {
            Id = detailData.Id,
            Title = detailData.Title,
            Description = detailData.Description,
            Genre = detailData.Genre,
            DirectorId = detailData.Director.Id,
            Actors = detailData.Actors.Select(a => a.Id).ToList()
        };

        AvailableDirectors = (await _personService.GetPersonsAsync()).Select(person => new PersonMetaData
        {
            Id = person.Id,
            Firstname = person.Firstname,
            Lastname = person.Lastname
        });

        SelectedDirectorId = AvailableDirectors.FirstOrDefault(d => _movieDetails.DirectorId == d.Id)?.Id ?? 0;
    }

    private int SelectedDirectorId
    {
        get => _movieDetails.DirectorId;
        set => _movieDetails.DirectorId = value;
    }

    private void OnDirectorChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value.ToString(), out int directorId))
        {
            _movieDetails.DirectorId = directorId;
        }
    }

    private async Task OnUpdateMovie()
    {
        await _movieService.UpdateMovieAsync(_movieDetails);
        StateHasChanged();
        NavigationManager.NavigateTo("/movies");
    }

    private void GoBack()
    {
        NavigationManager.NavigateTo("/movies");
    }
}
