using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBlog.Domain.Entities;

namespace SuperBlog.Domain.Abstract
{
    public interface IUrlManager
    {
        string GetEditUrl(Post post);
        string GetEditUrl(Category category);
    }
}
