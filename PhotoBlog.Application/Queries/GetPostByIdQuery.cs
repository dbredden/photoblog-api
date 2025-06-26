
using MediatR;
using PhotoBlog.Application.DTOs;
using PhotoBlog.Application.Interfaces;

namespace PhotoBlog.Application.Queries;

public class GetPostByIdQuery : IRequest<PostSummaryDto>
{
    public Guid Id { get; set; }
    public GetPostByIdQuery(Guid postId)
    {
        Id = postId;
    }
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostSummaryDto> 
    {
        private readonly IPostRepository _repository;

        public GetPostByIdQueryHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<PostSummaryDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetByIdAsync(request.Id);

            if (post == null)
                return null;

            return new PostSummaryDto
            {
                Id = post.Id,
                PhotoWebUrl = post.PhotoWebUrl,
                Location = post.Location,
                Description = post.Description,
                Date = post.Date
            };
        }
    }

}
