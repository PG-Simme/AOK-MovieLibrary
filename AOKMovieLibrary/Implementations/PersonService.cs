using AOKMovieLibrary.Abstractions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

namespace AOKMovieLibrary.Implementations;

public class PersonService : IPersonService
{
    private readonly IDbContextFactory<MovieContext> _contextFactory;
    private readonly MovieContext _context;
    private HubConnection _hubConnection;

    public PersonService(IDbContextFactory<MovieContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _context = contextFactory.CreateDbContext();

        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7108/personhub")
            .Build();
    }

    public void SeedData(IEnumerable<Person> persons)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Persons.AddRange(persons);
        context.SaveChanges();
    }

    public async Task<List<Person>> GetPersonsAsync()
    {
        List<Person> persons = [];

        _hubConnection.On<List<Person>>("ReceivePersons", (data) =>
        {
            persons = data;
        });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("GetPersons");

        return persons;
    }

    public async Task<Person> GetPersonAsync(int id)
    {
        Person person = new();

        _hubConnection.On<Person>("ReceivePersonDetails", (data) =>
        {
            person = data;
        });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("GetPersonDetails", id);

        return person;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        Person createdPerson = new();

        _hubConnection.On<Person>("ReceiveCreatePersonResult", (data) =>
        {
            createdPerson = data;
        });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("CreatePerson", person);

        return createdPerson;
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        Person updatedPerson = new();

        _hubConnection.On<Person>("ReceiveUpdatePersonResult", (data) =>
        {
            updatedPerson = data;
        });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("UpdatePerson", person);

        return updatedPerson;
    }

    public async Task DeletePersonAsync(int id)
    {
        _hubConnection.On("ReceiveDeletePersonResult", () => { });

        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.InvokeAsync("DeletePerson", id);
    }
}
