using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Entities;
using UrlShortenerApi.Services;

namespace UrlShortenerApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(UrlShorteningService.CharsInShortLink);
                builder.HasIndex(s => s.Code).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
