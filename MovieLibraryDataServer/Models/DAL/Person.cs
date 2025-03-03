namespace MovieLibraryDataServer.Models.DAL;

public class Person
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string Lastname { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    public override string ToString()
    {
        return $"{Firstname} {Lastname}";
    }
}
