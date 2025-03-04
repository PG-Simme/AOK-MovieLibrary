namespace AOKMovieLibrary.Frontend.Pages;

public partial class Register
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IAuthorizationService _authService { get; set; } = null!;

    private RegisterCommand _registerCommand = new();
    private string _message = string.Empty;

    private async Task HandleRegister()
    {
        bool success = await _authService.RegisterAsync(_registerCommand);

        if (success)
        {
            _message = string.Empty;
            NavigationManager.NavigateTo("/");
        }
        else
        {
            _message = "Registrierung fehlgeschlagen";
        }
    }
}
