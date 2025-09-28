using TodoApi.Middleware;

namespace TodoApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                // Use permissive CORS in development
                app.UseCors("DevelopmentCors");
            }
            else
            {
                // Use configured CORS origins in production
                app.UseCors("AllowedOrigins");
            }

            app.UseHttpsRedirection();

            // Add IP whitelist middleware (only in production)
            if (!app.Environment.IsDevelopment())
            {
                app.UseMiddleware<IpWhitelistMiddleware>();
            }

            app.MapControllers();

            return app;
        }
    }
}