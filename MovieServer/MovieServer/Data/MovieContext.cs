using Microsoft.EntityFrameworkCore;
using MovieServer.Models;

namespace MovieServer.Data
{
    public class MovieContext : DbContext
    {
           public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }
          public DbSet<Movie> Movies { get; set; }

    }
}
