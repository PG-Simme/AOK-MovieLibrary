namespace AOKMovieLibrary.Tests.Fixtures;

public class ServiceFixture : IDisposable
{
    public ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        return services.BuildServiceProvider();
    }

    public void Dispose()
    {
    }
}
