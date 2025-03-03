using Microsoft.AspNetCore.SignalR;

namespace MovieLibraryDataServer.Hubs;

public class PersonHub : Hub
{
    private readonly IPersonService _personService;

    public PersonHub(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task GetPersons()
    {
        await Clients.All.SendAsync("ReceivePersons", await _personService.GetPersonsAsync());
    }

    public async Task GetPersonDetails(int id)
    {
        await Clients.All.SendAsync("ReceivePersonDetails", await _personService.GetPersonAsync(id));
    }

    public async Task CreatePerson(Person createPersonCommand)
    {
        await Clients.Caller.SendAsync("ReceiveCreatePersonResult", await _personService.CreatePersonAsync(createPersonCommand));
    }

    public async Task UpdatePerson(Person updatePersonCommand)
    {
        await Clients.Caller.SendAsync("ReceiveUpdatePersonResult", await _personService.UpdatePersonAsync(updatePersonCommand));
    }

    public async Task DeletePerson(int id)
    {
        await Clients.Caller.SendAsync("ReceiveDeletePersonResult", _personService.DeletePersonAsync(id));
    }
}
