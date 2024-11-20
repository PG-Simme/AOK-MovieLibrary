namespace AOKMovieLibrary.Tests.MappingTests;

public class MovieMappingTests
{
    [Fact]
    public void MovieMapping_MovieToMovieDetails_IsValid()
    {
        // Arrange
        var movie = MovieFactory.CreateValidMovie().Generate();

        // Act
        var movieDetails = movie.MapToMovieDetails();

        // Assert
        Assert.Equal(movie.Id, movieDetails.Id);
        Assert.Equal(movie.Title, movieDetails.Title);
        Assert.Equal(movie.Genre, movieDetails.Genre);
        Assert.Equal(movie.Year, movieDetails.Year);
        Assert.Equal(movie.Director.MapToPersonMetaData(), movieDetails.Director);
        Assert.Equal(movie.Actors.Select(a => a.MapToPersonMetaData()), movieDetails.Actors);
        Assert.Equal(movie.Description, movieDetails.Description);
        Assert.Equal(movie.Runtime, movieDetails.Runtime);
    }

    [Fact]
    public void MovieMapping_MovieToMovieOverviewData_IsValid()
    {
        // Arrange
        var movie = MovieFactory.CreateValidMovie().Generate();

        // Act
        var movieOverviewData = movie.MapToMovieOverview();

        // Assert
        Assert.Equal(movie.Id, movieOverviewData.Id);
        Assert.Equal(movie.Title, movieOverviewData.Title);
        Assert.Equal(movie.Genre, movieOverviewData.Genre);
        Assert.Equal(movie.Year, movieOverviewData.Year);
        Assert.Equal(movie.Director.MapToPersonMetaData(), movieOverviewData.Director);
        Assert.Equal(movie.Actors.Select(a => a.MapToPersonMetaData()), movieOverviewData.Actors);
        Assert.Equal(movie.Description, movieOverviewData.Description);
        Assert.Equal(movie.Runtime, movieOverviewData.Runtime);
    }
}