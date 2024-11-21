namespace AOKMovieLibrary.Implementations;

public class MovieService : IMovieService
{
    private List<Movie> _movies = [];

    public MovieService(IPersonService personService)
    {
        var persons = new List<Person>
        {
            new Person { Id = 1, Firstname = "Christopher", Lastname = "Nolan" },
            new Person { Id = 2, Firstname = "Leonardo", Lastname = "DiCaprio" },
            new Person { Id = 3, Firstname = "Joseph", Lastname = "Gordon-Levitt" },
            new Person { Id = 4, Firstname = "Ellen", Lastname = "Page" },
            new Person { Id = 5, Firstname = "Lana", Lastname = "Wachowski" },
            new Person { Id = 6, Firstname = "Keanu", Lastname = "Reeves" },
            new Person { Id = 7, Firstname = "Laurence", Lastname = "Fishburne" },
            new Person { Id = 8, Firstname = "Carrie-Anne", Lastname = "Moss" },
            new Person { Id = 9, Firstname = "Francis", Lastname = "Coppola" },
            new Person { Id = 10, Firstname = "Marlon", Lastname = "Brando" },
            new Person { Id = 11, Firstname = "Al", Lastname = "Pacino" },
            new Person { Id = 12, Firstname = "James", Lastname = "Caan" },
            new Person { Id = 13, Firstname = "Quentin", Lastname = "Tarantino" },
            new Person { Id = 14, Firstname = "John", Lastname = "Travolta" },
            new Person { Id = 15, Firstname = "Uma", Lastname = "Thurman" },
            new Person { Id = 16, Firstname = "Samuel", Lastname = "Jackson" },
            new Person { Id = 17, Firstname = "Frank", Lastname = "Darabont" },
            new Person { Id = 18, Firstname = "Tim", Lastname = "Robbins" },
            new Person { Id = 19, Firstname = "Morgan", Lastname = "Freeman" },
            new Person { Id = 20, Firstname = "Bob", Lastname = "Gunton" }
        };

        personService.SeedData(persons);

        _movies = new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Title = "Inception",
                Genre = MovieGenre.SciFi | MovieGenre.Action,
                Year = 2010,
                Director = persons.First(p => p.Id == 1),
                Actors = persons.Where(p => new[] { 2, 3, 4 }.Contains(p.Id)).ToList(),
                Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                Runtime = 148
            },
            new Movie
            {
                Id = 2,
                Title = "The Matrix",
                Genre = MovieGenre.SciFi | MovieGenre.Action,
                Year = 1999,
                Director = persons.First(p => p.Id == 5),
                Actors = persons.Where(p => new[] { 6, 7, 8 }.Contains(p.Id)).ToList(),
                Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                Runtime = 136
            },
            new Movie
            {
                Id = 3,
                Title = "The Godfather",
                Genre = MovieGenre.Drama | MovieGenre.Crime,
                Year = 1972,
                Director = persons.First(p => p.Id == 9),
                Actors = persons.Where(p => new[] { 10, 11, 12 }.Contains(p.Id)).ToList(),
                Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                Runtime = 175
            },
            new Movie
            {
                Id = 4,
                Title = "Pulp Fiction",
                Genre = MovieGenre.Drama | MovieGenre.Crime,
                Year = 1994,
                Director = persons.First(p => p.Id == 13),
                Actors = persons.Where(p => new[] { 14, 15, 16 }.Contains(p.Id)).ToList(),
                Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                Runtime = 154
            },
            new Movie
            {
                Id = 5,
                Title = "The Shawshank Redemption",
                Genre = MovieGenre.Drama,
                Year = 1994,
                Director = persons.First(p => p.Id == 17),
                Actors = persons.Where(p => new[] { 18, 19, 20 }.Contains(p.Id)).ToList(),
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                Runtime = 142
            }
        };
    }

    public void SeedData(IEnumerable<Movie> movies)
    {
        _movies = movies.ToList();
    }

    public async Task<List<Movie>> GetMoviesAsync()
    {
        return _movies;
    }

    public async Task<List<MovieOverviewData>> GetMoviesForOverviewAsync()
    {
        return _movies.Select(m => m.MapToMovieOverview()).ToList();
    }

    public async Task<Movie> GetMovieAsync(int id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);

        if (movie == null)
        {
            throw new InvalidOperationException($"Movie with id {id} not found");
        }

        return movie;
    }

    public async Task<MovieDetailData> GetMovieDetailsAsync(int id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);

        if (movie == null)
        {
            throw new InvalidOperationException($"Movie with id {id} not found");
        }

        return movie.MapToMovieDetails();
    }

    public async Task<Movie> CreateMovieAsync(CreateMovieCommand movie)
    {
        Movie newMovie = movie.MapToMovie();
        if (_movies.Count == 0)
        {
            newMovie.Id = 0;
        }
        else
        {
            newMovie.Id = _movies.Max(m => m.Id) + 1;
        }

        _movies.Add(newMovie);
        return newMovie;
    }

    public async Task<Movie> UpdateMovieAsync(Movie movie)
    {
        var existingMovie = _movies.FirstOrDefault(m => m.Id == movie.Id);

        if (existingMovie == null)
        {
            throw new InvalidOperationException($"Movie with id {movie.Id} not found");
        }

        existingMovie.Title = movie.Title;
        existingMovie.Genre = movie.Genre;
        existingMovie.Year = movie.Year;
        existingMovie.Director = movie.Director;
        existingMovie.Actors = movie.Actors;
        existingMovie.Description = movie.Description;
        existingMovie.Runtime = movie.Runtime;

        return existingMovie;
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);
        if (movie != null)
        {
            _movies.Remove(movie);
        }
    }
}
