using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhotoBlog.Application.Commands;
using PhotoBlog.Application.Queries;
using PhotoBlog.Domain.Entities;
using System.Globalization;
using System.Threading.Tasks;


namespace PhotoBlog.API.Routes;

public static class Posts
{
    public static async Task<IEndpointRouteBuilder> MapPosts(this IEndpointRouteBuilder app)
    {
        // Post Post
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

        // Get All Posts
        app.MapGet("/api/posts", async ([FromServices] ISender mediator) =>
        {
            var posts = await mediator.Send(new GetAllPostsQuery());
            return Results.Ok(posts);
        });

        // Get Post By Id
        app.MapGet("api/posts/{id}", async ([FromServices] ISender mediator, Guid postId) =>
        {
            var posts = await mediator.Send(new GetPostByIdQuery(postId));
            return Results.Ok(posts);
        });


        // Update Post
        app.MapPut("api/posts/{id}", async ([FromServices] ISender mediator, Guid postId, [FromBody] PostEntity post) =>
        {
            var result = await mediator.Send(new UpdatePostCommand(postId, post));
            return Results.Ok(result);
        });

        return app;
    }

}
