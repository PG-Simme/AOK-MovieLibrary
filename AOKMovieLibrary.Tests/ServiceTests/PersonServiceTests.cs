namespace MovieLibrary.Tests.ServiceTests;

public class PersonServiceTests : IClassFixture<ServiceFixture>
{
    private readonly IPersonService _personService;

    public PersonServiceTests(ServiceFixture fixture)
    {
        ServiceProvider serviceProvider = fixture.CreateServiceProvider();
        _personService = serviceProvider.GetService<IPersonService>();
    }

    [Fact]
    public async Task GetPersonsAsync_ShouldReturnAllPersons()
    {
        // Arrange
        var personList = PersonFactory.CreateValidPerson().Generate(5);
        _personService.SeedData(personList);

        // Act
        var persons = await _personService.GetPersonsAsync();

        // Assert
        Assert.NotNull(persons);
        Assert.Equal(5, persons.Count);
    }

    [Fact]
    public async Task GetPersonAsync_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange
        var personList = PersonFactory.CreateValidPerson().Generate(5);
        _personService.SeedData(personList);

        // Act
        var person = await _personService.GetPersonAsync(0);

        // Assert
        Assert.Equal(personList.First(), person);
    }

    [Fact]
    public async Task GetPersonAsync_ShouldThrowException_WhenPersonDoesNotExist()
    {
        // Arrange
        Task getPersonTask = _personService.GetPersonAsync(999);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => getPersonTask);
    }

    [Fact]
    public async Task CreatePersonAsync_ShouldAddNewPerson()
    {
        // Arrange
        var newPerson = PersonFactory.CreateValidPerson().Generate();

        // Act
        var createdPerson = await _personService.CreatePersonAsync(newPerson);

        // Assert
        Assert.NotNull(createdPerson);
    }

    [Fact]
    public async Task CreatePersonAsync_ShouldThrowExceptionForNullperson()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _personService.CreatePersonAsync(null!));
    }

    [Fact]
    public async Task UpdatePersonAsync_ShouldUpdateExistingPerson()
    {
        // Arrange
        var personList = PersonFactory.CreateValidPerson().Generate(5);
        _personService.SeedData(personList);
        var person = await _personService.GetPersonAsync(0);
        person.Firstname = "UpdatedName";

        // Act
        var updatedPerson = await _personService.UpdatePersonAsync(person);

        // Assert
        Assert.Equal("UpdatedName", updatedPerson.Firstname);
    }

    [Fact]
    public async Task UpdatePersonAsync_ShouldThrowException_WhenPersonDoesNotExist()
    {
        // Arrange
        var nonExistentPerson = PersonFactory.CreateValidPerson().Generate();
        nonExistentPerson = await _personService.CreatePersonAsync(nonExistentPerson);
        var updatedPerson = PersonFactory.CreateValidPerson().Generate();
        updatedPerson.Id = 999;

        // Act
        Task updatePersonTask = _personService.UpdatePersonAsync(updatedPerson);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => updatePersonTask);
    }

    [Fact]
    public async Task DeletePersonAsync_ShouldRemovePerson()
    {
        // Arrange
        var personList = PersonFactory.CreateValidPerson().Generate(5);
        _personService.SeedData(personList);

        // Act
        await _personService.DeletePersonAsync(0);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _personService.GetPersonAsync(0));
    }

    [Fact]
    public async Task DeletePersonAsync_ShouldDoNothing_WhenPersonDoesNotExist()
    {
        // Arrange
        var personList = PersonFactory.CreateValidPerson().Generate(5);
        _personService.SeedData(personList);

        // Act
        await _personService.DeletePersonAsync(999);
        var persons = await _personService.GetPersonsAsync();

        // Assert
        Assert.Equal(personList.Count, persons.Count);
    }
}
