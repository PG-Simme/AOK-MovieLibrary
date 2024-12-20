namespace AOKMovieLibrary.Models.Commands;

public class CreateMovieCommand
{
    [Required]
    public string Title { get; set; }

    [Required]
    public MovieGenre Genre { get; set; }

    public int DirectorId { get; set; } = new();

    public List<int> Actors { get; set; } = new();

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
            Year = command.Year,
            Runtime = command.Runtime,
            Description = command.Description
        };
    }
}

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.Genre)
            .IsInEnum().WithMessage("Genre is required and must be a valid value.");

        RuleFor(x => x.DirectorId)
            .NotNull().WithMessage("Director information is required.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1888, DateTime.Now.Year).WithMessage($"Year must be between 1888 and {DateTime.Now.Year}.");

        RuleFor(x => x.Runtime)
            .GreaterThan(0).WithMessage("Runtime must be greater than 0.");
    }
}