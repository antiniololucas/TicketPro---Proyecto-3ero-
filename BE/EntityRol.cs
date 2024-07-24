using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class EntityRol : Entity
    {
        public string Nombre { get; set; }
        public List<IPermiso> Permisos { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
