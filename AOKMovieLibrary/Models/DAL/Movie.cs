namespace AOKMovieLibrary.Models.DAL;

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; }

    public MovieGenre Genre { get; set; }

    public int Year { get; set; }

    public Person Director { get; set; }

    public List<Person> Actors { get; set; }

    public string? Description { get; set; }

    public int Runtime { get; set; }
}

[Flags]
public enum MovieGenre
{
    None = 0,
    Action = 1 << 0, // 1
    Comedy = 1 << 1, // 2
    Drama = 1 << 2, // 4
    Fantasy = 1 << 3, // 8
    Horror = 1 << 4, // 16
    Mystery = 1 << 5, // 32
    Romance = 1 << 6, // 64
    Thriller = 1 << 7, // 128
    SciFi = 1 << 8, // 256
    Documentary = 1 << 9, // 512
    Animation = 1 << 10, // 1024
    Adventure = 1 << 11, // 2048
    Musical = 1 << 12, // 4096
    Biography = 1 << 13, // 8192
    Crime = 1 << 14, // 16384
    Family = 1 << 15, // 32768
    History = 1 << 16, // 65536
    War = 1 << 17, // 131072
    Western = 1 << 18, // 262144
    Sport = 1 << 19, // 524288
    Music = 1 << 20, // 1048576
}
