using Microsoft.Extensions.Options;

namespace AOKMovieLibrary.Frontend.Pages;

public partial class AddMovie
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IMovieService _movieService { get; set; } = null!;

    [Inject] private IPersonService _personService { get; set; } = null!;

    private CreateMovieCommand NewMovie = new()
    {
        Genre = MovieGenre.Action,
        Director = new PersonMetaData(),
        Actors = new List<PersonMetaData>()
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
    }

    private int SelectedDirectorId
    {
        get => NewMovie.Director?.Id ?? 0;
        set => NewMovie.Director = AvailableDirectors.FirstOrDefault(d => d.Id == value);
    }

    private void OnDirectorChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value.ToString(), out int directorId))
        {
            NewMovie.Director = AvailableDirectors.FirstOrDefault(d => d.Id == directorId);
        }
    }

    private void NavigateBack()
    {
        NavigationManager.NavigateTo("/movies");
    }

    private void OnAddMovie()
    {
        _movieService.CreateMovieAsync(NewMovie);
        NavigationManager.NavigateTo("/movies");
    }
}
