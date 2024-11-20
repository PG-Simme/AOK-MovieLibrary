namespace AOKMovieLibrary.Tests.MappingTests;

public class PersonMappingTests
{
    [Fact]
    public void PersonMapping_PersonToPersonMetaData_IsValid()
    {
        // Arrange
        var person = PersonFactory.CreateValidPerson().Generate();

        // Act
        var personDetails = person.MapToPersonMetaData();

        // Assert
        Assert.Equal(person.Id, personDetails.Id);
        Assert.Equal(person.Firstname, personDetails.Firstname);
        Assert.Equal(person.Lastname, personDetails.Lastname);
    }
}
