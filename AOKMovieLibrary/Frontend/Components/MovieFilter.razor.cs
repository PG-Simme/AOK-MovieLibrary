namespace AOKMovieLibrary.Frontend.Components;

public partial class MovieFilter
{
    [CascadingParameter] public MovieGenre GenreFilter { get; set; }

    [Parameter] public EventCallback<MovieGenre> OnGenreFilterChanged { get; set; }

    private async Task GenreFilterChanged(ChangeEventArgs e)
    {
        if (string.IsNullOrEmpty(e.Value?.ToString()))
        {
            GenreFilter = MovieGenre.None;
            await OnGenreFilterChanged.InvokeAsync(MovieGenre.None);
            return;
        }

        MovieGenre genre = Enum.Parse<MovieGenre>(e.Value.ToString());

        GenreFilter = genre;
        await OnGenreFilterChanged.InvokeAsync(genre);
    }
}
