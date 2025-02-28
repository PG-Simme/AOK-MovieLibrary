namespace AOKMovieLibrary.Models.DAL;

public class User
{
    public int Id { get; set; }
    
    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }
    
    public string? Firstname { get; set; }
    
    public string? Lastname { get; set; }
    
    [Timestamp]
    public byte[] RowVersion { get; set; }
    
    public override string ToString()
    {
        return $"{Firstname} {Lastname}";
    }
}
