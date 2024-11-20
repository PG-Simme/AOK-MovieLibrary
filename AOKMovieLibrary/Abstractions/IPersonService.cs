namespace AOKMovieLibrary.Abstractions;

public interface IPersonService
{
    void SeedData(IEnumerable<Person> persons);

    Task<List<Person>> GetPersonsAsync();

    Task<Person> GetPersonAsync(int id);

    Task<Person> CreatePersonAsync(Person person);

    Task<Person> UpdatePersonAsync(Person person);

    Task DeletePersonAsync(int id);
}
