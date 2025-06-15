using MediatR;
using PhotoBlog.Application.Commands;
using Microsoft.AspNetCore.Mvc;


namespace PhotoBlog.API.Routes;

public static class Posts
{
    public static IEndpointRouteBuilder MapPosts(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/posts", async (
            HttpRequest request,
            [FromServices] ISender mediator) =>
        {
            var form = await request.ReadFormAsync();
            var image = form.Files.GetFile("image");
            var location = form["location"];
            var description = form["description"];
            var date = DateTime.Parse(form["date"]);

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

    }

}
