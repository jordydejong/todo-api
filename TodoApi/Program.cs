using TodoApi.Extensions;
using TodoApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiDocumentation();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Apply database migrations with retry logic
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    var retries = 0;
    const int maxRetries = 10;

    while (retries < maxRetries)
    {
        try
        {
            logger.LogInformation("Attempting to connect to database...");
            dbContext.Database.EnsureCreated();
            logger.LogInformation("Database connection successful!");
            break;
        }
        catch (Exception ex)
        {
            retries++;
            logger.LogWarning($"Database connection failed (attempt {retries}/{maxRetries}): {ex.Message}");

            if (retries >= maxRetries)
            {
                logger.LogError("Failed to connect to database after maximum retries.");
                throw;
            }

            logger.LogInformation("Waiting 5 seconds before retry...");
            Thread.Sleep(5000);
        }
    }
}

app.ConfigurePipeline();

app.Run();
