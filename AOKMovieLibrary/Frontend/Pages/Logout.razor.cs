namespace AOKMovieLibrary.Frontend.Pages;

public partial class Logout
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IAuthorizationService _authService { get; set; } = null!;

    private string _message = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        await _authService.LogoutAsync();

        NavigationManager.NavigateTo("/", forceLoad: true);
    }
}
