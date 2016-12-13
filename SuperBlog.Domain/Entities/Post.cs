using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBlog.Domain.Entities
{
    public enum PostStates
    {
        OnModeration = 0
    }
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public byte[] MainPicture { get; set; }
        public int State { get; set; }

        public override string ToString()
        {
            return Title + " : " + Text.Substring(0, Text.Length > 20 ? 20 : Text.Length) + "...";
        }
    }
}
