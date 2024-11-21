namespace AOKMovieLibrary.Models.Commands;

public class CreateMovieCommand
{
    [Required]
    public string Title { get; set; }

    [Required]
    public MovieGenre Genre { get; set; }

    public PersonMetaData Director { get; set; } = new();

    public List<PersonMetaData> Actors { get; set; } = new();

    public int Year { get; set; }

    public int Runtime { get; set; }

    public string? Description { get; set; }
}

public static class CreateMovieCommandMapping
{
    public static Movie MapToMovie(this CreateMovieCommand command)
    {
        return new Movie
        {
            Title = command.Title,
            Genre = command.Genre,
            Director = new Person
            {
                Id = command.Director.Id,
                Firstname = command.Director.Firstname,
                Lastname = command.Director.Lastname
            },
            Actors = command.Actors.Select(actor => new Person
            {
                Id = actor.Id,
                Firstname = actor.Firstname,
                Lastname = actor.Lastname
            }).ToList(),
            Year = command.Year,
            Runtime = command.Runtime,
            Description = command.Description
        };
    }
}
