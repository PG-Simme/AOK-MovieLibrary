using AOKMovieLibrary.ProjectConfiguration;

namespace AOKMovieLibrary.Tests.Fixtures;

public class ServiceFixture : IDisposable
{
    public ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddLibraryServices();

        return services.BuildServiceProvider();
    }

    public void Dispose()
    {
    }
}
