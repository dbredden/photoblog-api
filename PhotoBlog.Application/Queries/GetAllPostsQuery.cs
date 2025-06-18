using MediatR;
using PhotoBlog.Application.DTOs;
using PhotoBlog.Application.Interfaces;

namespace PhotoBlog.Application.Queries;

public class GetAllPostsQuery : IRequest<List<PostSummaryDto>>
{

    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, List<PostSummaryDto>>
    {
        private readonly IPostRepository _repository;

        public GetAllPostsQueryHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PostSummaryDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetAllAsync();

            return posts.Select(p => new PostSummaryDto
            {
                Id = p.Id,
                PhotoWebUrl = p.PhotoWebUrl,
                Location = p.Location,
                Description = p.Description,
                Date = p.Date
            }).ToList();
        }
    }

}
