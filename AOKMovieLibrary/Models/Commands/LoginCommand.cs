namespace AOKMovieLibrary.Models.Commands;

public class LoginCommand
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
