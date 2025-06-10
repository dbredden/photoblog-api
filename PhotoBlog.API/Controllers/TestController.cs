using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.OpenApi;

namespace PhotoBlog.API.Routes;

public static class TestRoutes
{
    public static IEndpointRouteBuilder MapTestRoutes(this IEndpointRouteBuilder app)
    {
        string[] photos = ["Photo 1", "Photo 2", "Photo 3"];
        var random = new Random();

        app.MapGet("/random-photo", () =>
        {
            var photo = photos[random.Next(photos.Length)];
            return Results.Ok(new { photo });
        })
        .WithName("GetRandomPhoto");

        return app;
    }
}
