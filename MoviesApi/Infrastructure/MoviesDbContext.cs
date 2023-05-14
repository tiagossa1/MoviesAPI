using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class MoviesDbContext : DbContext
{
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseInMemoryDatabase("MoviesDb");
    // }

    public DbSet<Gender> Genders { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<MovieCast> MovieCasts { get; set; } = null!;
    public DbSet<Person> People { get; set; } = null!;
}