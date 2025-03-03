using MovieLibraryDataServer.Abstractions;
using MovieLibraryDataServer.Implementations;

namespace MovieLibraryDataServer.ProjectConfiguration;

public static class LibraryExtensions
{
    public static IServiceCollection AddLibraryServices(this IServiceCollection services)
    {
        // Add services here
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IPersonService, PersonService>();

        services.AddScoped<MovieStateService>();

        return services;
    }
}
