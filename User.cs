using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP_Login_Page
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public User(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
