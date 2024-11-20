namespace AOKMovieLibrary.Frontend.Components;

public partial class MovieCard
{
    [Parameter] public MovieOverviewData Movie { get; set; } = default!;

    [Parameter] public EventCallback<MovieOverviewData> OnDetails { get; set; }

    private async Task OnDetailsClick()
    {
        if (OnDetails.HasDelegate)
        {
            await OnDetails.InvokeAsync(Movie);
        }
    }
}
