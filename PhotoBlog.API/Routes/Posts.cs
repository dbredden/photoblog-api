using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhotoBlog.Application.Commands;
using PhotoBlog.Application.Queries;
using System.Globalization;


namespace PhotoBlog.API.Routes;

public static class Posts
{
    public static IEndpointRouteBuilder MapPosts(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/posts", async (HttpRequest request, [FromServices] ISender mediator) =>
        {
            var form = await request.ReadFormAsync();
            var image = form.Files.GetFile("image");
            var location = form["location"];
            var description = form["description"];
            var date = DateTime.ParseExact(form["date"], "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var command = new CreatePostCommand
            {
                Image = image,
                Location = location,
                Description = description,
                Date = date
            };

            var postId = await mediator.Send(command);

            return Results.Created($"/api/posts/{postId}", new { postId });
            
        });

        app.MapGet("/api/posts", async ([FromServices] ISender mediator) =>
        {
            var posts = await mediator.Send(new GetAllPostsQuery());
            return Results.Ok(posts);
        });


        app.MapGet("api/posts/{id}", async ([FromServices] ISender mediator) =>
        {
            var posts = await mediator.Send(new GetPostByIdQuery());
            return Results.Ok(posts);
        });

        return app;
    }

}
