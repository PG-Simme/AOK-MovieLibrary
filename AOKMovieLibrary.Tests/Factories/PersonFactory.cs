namespace AOKMovieLibrary.Tests.Factories;

internal class PersonFactory
{
    internal static Faker<Person> CreateValidPerson()
    {
        return new Faker<Person>()
            .RuleFor(p => p.Id, f => f.IndexFaker)
            .RuleFor(p => p.Firstname, f => f.Name.FirstName())
            .RuleFor(p => p.Lastname, f => f.Name.LastName());
    }
}
