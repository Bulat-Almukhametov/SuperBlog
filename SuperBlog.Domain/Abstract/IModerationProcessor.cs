using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SuperBlog.Domain.Abstract
{
    public interface IModerationProcessor
    {
        void ProcessModeration(string content, string moderationUrl);
    }
}
