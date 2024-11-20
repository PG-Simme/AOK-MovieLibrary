namespace AOKMovieLibrary.Models.Responses;

public record PersonMetaData
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string Lastname { get; set; }

    public override string ToString()
    {
        return $"{Firstname} {Lastname}";
    }
}

public static class PersonMetaDataMapping
{
    public static PersonMetaData MapToPersonMetaData(this Person person)
    {
        return new PersonMetaData
        {
            Id = person.Id,
            Firstname = person.Firstname,
            Lastname = person.Lastname
        };
    }
}
