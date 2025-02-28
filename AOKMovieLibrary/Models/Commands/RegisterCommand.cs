namespace AOKMovieLibrary.Models.Commands;

public class RegisterCommand
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public string? Email { get; set; }
    
    public string? Firstname { get; set; }
    
    public string? Lastname { get; set; }
}
