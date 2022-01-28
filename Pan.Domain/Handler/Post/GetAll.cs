using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pan.Domain.Handler.Accounts;
using Pan.Domain.Model.Common;
using Pan.Infrastructure.Context;
using Pan.Infrastructure.Entity;
using Pan.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pan.Domain.Handler.Post
{
    public class PostHandler : IRequestHandler<PostRequest, PagedResultDto<PostItmes>>
    {
        private readonly IRepository<post, int> _postRepository;
        private readonly IMapper _mapper;

        public PostHandler(IRepository<post, int> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<PostItmes>> Handle(PostRequest request, CancellationToken cancellationToken)
        {
            var query = _postRepository.GetQueryWithDisable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Title.Contains(request.Keyword) || x.htmlContext.Contains(request.Keyword));
            }
            var count = await query.CountAsync();
            var list = await query.OrderAndPagedAsync(request);
            
            var items = _mapper.Map<List<PostItmes>>(list);

            return new PagedResultDto<PostItmes>(items, count);
        }
    }

    public class PostRequest : PagedAndSortedRequest, IRequest<PagedResultDto<PostItmes>>
    {
        public string Keyword { get; set; }
    }

    public class PostItmes : EntityDto<int>
    {
        public string Title { get; set; }
        public string htmlContext { get; set; }
        public int? Tags { get; set; }
        public string request_id { get; set; }
        public string aliyun_url { get; set; }
        public string del_url { get; set; }
        public string next_url { get; set; }
        public string prev { get; set; }
        public string createTime { get; set; }
    }

}
