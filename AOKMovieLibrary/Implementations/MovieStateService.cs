namespace AOKMovieLibrary.Implementations;

public class MovieStateService
{
    public MovieGenre CurrentGenreFilter { get; set; }

    public void SetGenreFilter(MovieGenre genre)
    {
        CurrentGenreFilter = genre;
    }
}
