namespace AOKMovieLibrary.Frontend.Pages;

public partial class AddMovie
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IMovieService _movieService { get; set; } = null!;

    [Inject] private IPersonService _personService { get; set; } = null!;

    private CreateMovieCommand NewMovie = new()
    {
        Genre = MovieGenre.Action,
        Actors = []
    };

    private string SelectedGenre
    {
        get => NewMovie.Genre.ToString();
        set => NewMovie.Genre = Enum.Parse<MovieGenre>(value);
    }

    private IEnumerable<string> GenreOptions => Enum.GetNames<MovieGenre>();
    private IEnumerable<PersonMetaData> AvailableDirectors = [];

    protected override async Task OnInitializedAsync()
    {
        AvailableDirectors = (await _personService.GetPersonsAsync()).Select(person => new PersonMetaData
        {
            Id = person.Id,
            Firstname = person.Firstname,
            Lastname = person.Lastname
        });

        SelectedDirectorId = AvailableDirectors.FirstOrDefault()?.Id ?? 0;
    }

    private int SelectedDirectorId
    {
        get => NewMovie.DirectorId;
        set => NewMovie.DirectorId = value;
    }

    private void OnDirectorChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value.ToString(), out int directorId))
        {
            NewMovie.DirectorId = directorId;
        }
    }

    private void NavigateBack()
    {
        NavigationManager.NavigateTo("/movies");
    }

    private async Task OnAddMovie()
    {
        await _movieService.CreateMovieAsync(NewMovie);
        StateHasChanged();
        NavigationManager.NavigateTo("/movies");
    }
}
