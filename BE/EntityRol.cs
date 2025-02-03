using System.Collections.Generic;

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
