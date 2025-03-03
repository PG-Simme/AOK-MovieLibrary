using MovieLibraryDataServer.Models.DAL;

namespace MovieLibraryDataServer.Implementations;

public class MovieStateService
{
    public MovieGenre CurrentGenreFilter { get; set; }

    public void SetGenreFilter(MovieGenre genre)
    {
        CurrentGenreFilter = genre;
    }
}
