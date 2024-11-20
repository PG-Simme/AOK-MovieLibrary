namespace AOKMovieLibrary.Models.Commands;

public class CreateMovieCommand
{
}

public static class CreateMovieCommandMapping
{
    public static Movie MapToMovie(this CreateMovieCommand command)
    {
        return new Movie
        {
        };
    }
}
