using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBlog.Domain.Entities
{
    public enum CategoryStates
    {
        OnModeration = 0
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
