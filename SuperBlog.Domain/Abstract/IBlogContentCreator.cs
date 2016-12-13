using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBlog.Domain.Entities;

namespace SuperBlog.Domain.Abstract
{
    public enum CategoryCreateReturns
    {
        Success = 0,
        AlreadyExist = 1
    }
    public enum PostCreateReturns
    {
        Success = 0
    }
    public interface IBlogContentCreator
    {
        int CreatePost(Post post, IPostRepository postRepository);
        int CreateCategory(Category category, ICategoryRepository categoryRepository);
    }
}
