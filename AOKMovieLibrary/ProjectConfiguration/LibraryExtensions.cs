namespace AOKMovieLibrary.ProjectConfiguration;

public static class LibraryExtensions
{
    public static IServiceCollection AddLibraryServices(this IServiceCollection services)
    {
        // Add services here
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IPersonService, PersonService>();

        return services;
    }
}
