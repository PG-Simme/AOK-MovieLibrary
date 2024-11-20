namespace AOKMovieLibrary.Tests.Factories;

internal class MovieFactory
{
    internal static Faker<Movie> CreateValidMovie()
    {
        Faker<Movie> faker = new Faker<Movie>()
            .RuleFor(m => m.Id, f => f.IndexFaker)
            .RuleFor(m => m.Title, f => f.Lorem.Sentence())
            .RuleFor(m => m.Genre, f => f.PickRandom<MovieGenre>())
            .RuleFor(m => m.Year, f => f.Random.Int(1900, 2022))
            .RuleFor(m => m.Director, f => PersonFactory.CreateValidPerson().Generate())
            .RuleFor(m => m.Actors, f => PersonFactory.CreateValidPerson().Generate(3))
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.Runtime, f => f.Random.Int(60, 180));

        return faker;
    }
}
