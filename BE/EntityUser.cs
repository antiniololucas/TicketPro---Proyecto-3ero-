using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class EntityUser : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsBlock { get; set; }
    }
}
