using Microsoft.EntityFrameworkCore;
using MovieLibraryDataServer.Abstractions;
using MovieLibraryDataServer.Models.DAL;

namespace MovieLibraryDataServer.Implementations;

public class PersonService : IPersonService
{
    private readonly IDbContextFactory<MovieContext> _contextFactory;
    private readonly MovieContext _context;

    public PersonService(IDbContextFactory<MovieContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _context = contextFactory.CreateDbContext();
    }

    public void SeedData(IEnumerable<Person> persons)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Persons.AddRange(persons);
        context.SaveChanges();
    }

    public async Task<List<Person>> GetPersonsAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Persons.ToListAsync();
    }

    public async Task<Person> GetPersonAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
        {
            throw new InvalidOperationException($"Person with id {id} not found");
        }

        return person;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Persons.Add(person);
        await context.SaveChangesAsync();

        return person;
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        using var context = _contextFactory.CreateDbContext();
        var existingPerson = await context.Persons.FirstOrDefaultAsync(p => p.Id == person.Id);

        if (existingPerson == null)
        {
            throw new InvalidOperationException($"Person with id {person.Id} not found");
        }

        existingPerson.Firstname = person.Firstname;
        existingPerson.Lastname = person.Lastname;

        await context.SaveChangesAsync();

        return existingPerson;
    }

    public async Task DeletePersonAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
        {
            throw new InvalidOperationException($"Person with id {id} not found");
        }

        context.Persons.Remove(person);
        await context.SaveChangesAsync();
    }
}
