namespace AOKMovieLibrary.Abstractions;

public interface IAuthorizationService
{
    Task<bool> RegisterAsync(RegisterCommand command);

    Task<bool> LoginAsync(LoginCommand command);

    Task LogoutAsync();
}
