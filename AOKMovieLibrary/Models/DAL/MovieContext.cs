using Microsoft.EntityFrameworkCore;

namespace AOKMovieLibrary.Models.DAL;

public class MovieContext : DbContext
{
    public MovieContext()
    {

    }

    public MovieContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Person> Persons { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).IsRequired().HasMaxLength(50);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.HasData(
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
                );
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Year).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Runtime).IsRequired();
            entity.Property(e => e.RowVersion).IsRowVersion();

            entity.HasOne(e => e.Director)
                  .WithMany()
                  .HasForeignKey("DirectorId")
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Actors)
                  .WithMany()
                  .UsingEntity(j => j.ToTable("MovieActors"));
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(64);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.HasIndex(e => e.Username).IsUnique();
        });
    }
}
