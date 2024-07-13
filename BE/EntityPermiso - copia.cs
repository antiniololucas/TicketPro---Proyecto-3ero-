using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class EntityPermiso : IPermiso
    {
        public string Nombre { get; set; }
        public int Id { get; set; }
    }
}
