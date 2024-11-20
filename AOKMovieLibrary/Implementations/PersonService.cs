namespace AOKMovieLibrary.Implementations;

public class PersonService : IPersonService
{
    private List<Person> _persons = [];

    public void SeedData(IEnumerable<Person> persons)
    {
        _persons = persons.ToList();
    }

    public async Task<List<Person>> GetPersonsAsync()
    {
        return _persons;
    }

    public async Task<Person> GetPersonAsync(int id)
    {
        var person = _persons.FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            throw new InvalidOperationException($"Person with id {id} not found");
        }

        return person;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        if (_persons.Count == 0)
        {
            person.Id = 0;
        }
        else
        {
            person.Id = _persons.Max(p => p.Id) + 1;
        }

        _persons.Add(person);
        return person;
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var existingPerson = _persons.FirstOrDefault(p => p.Id == person.Id);

        if (existingPerson == null)
        {
            throw new InvalidOperationException($"Person with id {person.Id} not found");
        }

        existingPerson.Firstname = person.Firstname;
        existingPerson.Lastname = person.Lastname;

        return existingPerson;
    }

    public async Task DeletePersonAsync(int id)
    {
        var person = _persons.FirstOrDefault(p => p.Id == id);
        if (person != null)
        {
            _persons.Remove(person);
        }
    }
}
