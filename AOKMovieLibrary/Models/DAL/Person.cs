namespace AOKMovieLibrary.Models.DAL;

public class Person
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string Lastname { get; set; }

    public override string ToString()
    {
        return $"{Firstname} {Lastname}";
    }
}
