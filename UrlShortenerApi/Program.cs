using Microsoft.EntityFrameworkCore;
using UrlShortenerApi;
using UrlShortenerApi.Entities;
using UrlShortenerApi.Extensions;
using UrlShortenerApi.Models;
using UrlShortenerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure())
    );

builder.Services.AddScoped<UrlShorteningService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}


app.MapPost("api/shorten", async (
    ShortenUrlRequest request,
    UrlShorteningService urlService,
    AppDbContext dbContext,
    HttpContext httpContext) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
    {
        return Results.BadRequest("A url inserida é invalida");
    }

    var code = await urlService.GenerateUniqueCode();

    var shortenedUrl = new ShortenedUrl
    {
        Id = Guid.NewGuid(),
        LongUrl = request.Url,
        Code = code,
        ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/dev/api/{code}",
        CreatedAt = DateTime.Now
    };

    dbContext.ShortenedUrls.Add(shortenedUrl);

    await dbContext.SaveChangesAsync();

    return Results.Ok(shortenedUrl.ShortUrl);
});

app.MapGet("dev/api/{code}", async (string code, AppDbContext dbContext) =>
{
    var shortenedUrl = await dbContext.ShortenedUrls.FirstOrDefaultAsync(s => s.Code == code);

    if (shortenedUrl == null) 
    {
        return Results.NotFound();
    }

    return Results.Redirect(shortenedUrl.LongUrl);
});

app.UseHttpsRedirection();
app.Run();