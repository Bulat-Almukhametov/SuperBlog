using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBlog.Domain.Entities;

namespace SuperBlog.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        void SaveCategory(Category category);
        Category DeleteCategory(int categoryId);
    }
}
