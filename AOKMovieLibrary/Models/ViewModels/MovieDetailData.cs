namespace AOKMovieLibrary.Models.ViewModels;

public record MovieDetailData
{
    public int Id { get; set; }

    public string Title { get; set; }

    public MovieGenre Genre { get; set; }

    public int Year { get; set; }

    public PersonMetaData Director { get; set; }

    public List<PersonMetaData> Actors { get; set; }

    public string? Description { get; set; }

    public int Runtime { get; set; }
}

public static class MovieDetailsMapping
{
    public static MovieDetailData MapToMovieDetails(this Movie movie)
    {
        return new MovieDetailData
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre,
            Year = movie.Year,
            Director = movie.Director.MapToPersonMetaData(),
            Actors = movie.Actors.Select(a => a.MapToPersonMetaData()).ToList(),
            Description = movie.Description,
            Runtime = movie.Runtime
        };
    }
}
