using AOKMovieLibrary.Models.Commands;
using AOKMovieLibrary.Models.DAL;

namespace MovieLibrary.Tests.ServiceTests;

public class MovieServiceTests : IClassFixture<ServiceFixture>
{
    private readonly IMovieService _movieService;

    public MovieServiceTests(ServiceFixture fixture)
    {
        ServiceProvider serviceProvider = fixture.CreateServiceProvider();
        _movieService = serviceProvider.GetService<IMovieService>();
    }

    [Fact]
    public async Task GetMoviesAsync_ShouldReturnAllMovies()
    {
        // Arrange
        var movieList = MovieFactory.CreateValidMovie().Generate(10);
        _movieService.SeedData(movieList);

        // Act
        var movies = await _movieService.GetMoviesAsync();

        // Assert
        Assert.NotNull(movies);
        Assert.Equal(movieList.Count, movies.Count);
    }

    [Fact]
    public async Task GetMovieAsync_ShouldReturnCorrectMovie()
    {
        // Arrange
        var movieList = MovieFactory.CreateValidMovie().Generate(10);
        _movieService.SeedData(movieList);

        // Act
        var movie = await _movieService.GetMovieAsync(0);

        // Assert
        Assert.Equal(0, movie.Id);
        Assert.Equal(movieList.First().Title, movie.Title);
    }

    [Fact]
    public async Task GetMovieAsync_ShouldThrowExceptionForNonExistentMovie()
    {
        // Arrange
        Task getMovieTask = _movieService.GetMovieAsync(999);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => getMovieTask);
    }

    [Fact]
    public async Task CreateMovieAsync_ShouldAddNewMovie()
    {
        // Arrange
        var newMovie = MovieFactory.CreateValidMovie().Generate();

        // Act
        //var createdMovie = await _movieService.CreateMovieAsync(newMovie);

        //// Assert
        //Assert.NotNull(createdMovie);
    }

    [Fact]
    public async Task CreateMovieAsync_ShouldThrowExceptionForNullMovie()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _movieService.CreateMovieAsync(null!));
    }

    [Fact]
    public async Task UpdateMovieAsync_ShouldUpdateExistingMovie()
    {
        // Arrange
        var movieList = MovieFactory.CreateValidMovie().Generate(10);
        _movieService.SeedData(movieList);
        var movie = await _movieService.GetMovieDetailsAsync(0);

        UpdateMovieCommand updateMovie = new UpdateMovieCommand
        {
            Id = movie.Id,
            Title = "Updated Movie",
            Description = movie.Description,
            Genre = movie.Genre,
            DirectorId = movie.Director.Id,
            Actors = movie.Actors.Select(a => a.Id).ToList()
        };

        // Act
        var updatedMovie = await _movieService.UpdateMovieAsync(updateMovie);

        // Assert
        Assert.NotNull(movie);
        Assert.Equal(movie.Title, updatedMovie.Title);
        Assert.Equal(movie.Genre, updatedMovie.Genre);
        Assert.Equal(movie.Year, updatedMovie.Year);
        Assert.Equal(movie.Director, updatedMovie.Director);
        Assert.Equal(movie.Actors, updatedMovie.Actors);
        Assert.Equal(movie.Description, updatedMovie.Description);
        Assert.Equal(movie.Runtime, updatedMovie.Runtime);
    }

    [Fact]
    public async Task UpdateMovieAsync_ShouldThrowExceptionForNonExistentMovie()
    {
        // Arrange
        var nonExistentMovie = MovieFactory.CreateValidMovie().Generate();
        //nonExistentMovie = await _movieService.CreateMovieAsync(nonExistentMovie);
        var movie = MovieFactory.CreateValidMovie().Generate();

        UpdateMovieCommand updateMovie = new UpdateMovieCommand
        {
            Id = 999,
            Title = "Updated Movie",
            Description = movie.Description,
            Genre = movie.Genre,
            DirectorId = movie.Director.Id,
            Actors = movie.Actors.Select(a => a.Id).ToList()
        };

        // Act
        Task updateMovieTask = _movieService.UpdateMovieAsync(updateMovie);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => updateMovieTask);
    }

    [Fact]
    public async Task DeleteMovieAsync_ShouldRemoveMovie()
    {
        // Arrange
        var movieList = MovieFactory.CreateValidMovie().Generate(10);
        _movieService.SeedData(movieList);

        // Act
        await _movieService.DeleteMovieAsync(0);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _movieService.GetMovieAsync(0));
    }

    [Fact]
    public async Task DeleteMovieAsync_ShouldNotThrowForNonExistentMovie()
    {
        // Arrange
        var movieList = MovieFactory.CreateValidMovie().Generate(10);
        _movieService.SeedData(movieList);

        // Act
        await _movieService.DeleteMovieAsync(999);
        var movies = await _movieService.GetMoviesAsync();

        // Assert
        Assert.Equal(movieList.Count, movies.Count);
    }
}
