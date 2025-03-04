namespace AOKMovieLibrary.Frontend.Pages;

public partial class Login
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IAuthorizationService _authService { get; set; } = null!;

    [SupplyParameterFromForm]
    public LoginCommand LoginCommand { get; set; } = new();

    private string _message = string.Empty;

    private async Task HandleLogin()
    {
        var success = await _authService.LoginAsync(LoginCommand);

        if (success)
        {
            _message = string.Empty;
            NavigationManager.NavigateTo("/");
        }
        else
        {
            _message = "Invalid username or password";
        }
    }
}
