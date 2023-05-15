using GSN.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api;
internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
    public DbSet<Blog> Blogs { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecast>().HasData(
            new WeatherForecast
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                TemperatureC = 32,
                Summary = "Bright sunny day"
            });

        modelBuilder.Entity<Blog>(entity => { entity.Property(e => e.BlogName).IsRequired(); });

        #region BlogSeed
        modelBuilder.Entity<Blog>().HasData(new Blog { Id = "5b1c2b4d-48c7-402a-80c3-cc796ad49c6b", BlogName = "First Seeded Blog", CreatedOnDate = DateTime.UtcNow, Published = true });
        modelBuilder.Entity<Blog>().HasData(new Blog { Id = "d28888e9-2ba9-473a-a40f-e38cb54f9b35", BlogName = "Second Seeded Blog", CreatedOnDate = DateTime.UtcNow, Published = true });
        #endregion

        modelBuilder.Entity<Post>(
            entity =>
            {
                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey("BlogId");
            });

        #region PostSeed
        modelBuilder.Entity<Post>().HasData(new Post { BlogId = "d28888e9-2ba9-473a-a40f-e38cb54f9b35", Id = "493c3228-3444-4a49-9cc0-e8532edc59b2", Title = "First Post Seeded", Content = "Test 1 Seeded content" });
        #endregion

        #region AnonymousPostSeed
        modelBuilder.Entity<Post>().HasData(
            new { BlogId = "d28888e9-2ba9-473a-a40f-e38cb54f9b35", Id = "24810dfc-2d94-4cc7-aab5-cdf98b83f0c9", Title = "Second post", Content = "Test 2 Seeded anonymouspost content" });
        #endregion

        #region OwnedTypeSeed
        modelBuilder.Entity<Post>().OwnsOne(p => p.AuthorName).HasData(
            new { PostId = "493c3228-3444-4a49-9cc0-e8532edc59b2", First = "Andriy", Last = "Svyryd" },
            new { PostId = "24810dfc-2d94-4cc7-aab5-cdf98b83f0c9", First = "Diego", Last = "Vega" });
        #endregion
    }
}
