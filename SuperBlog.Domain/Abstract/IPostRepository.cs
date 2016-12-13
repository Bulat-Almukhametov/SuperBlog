using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBlog.Domain.Entities;

namespace SuperBlog.Domain.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        void SavePost(Post post);
        Post DeletePost(int postId);
    }
}
