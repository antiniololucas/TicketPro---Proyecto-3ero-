﻿using System.Collections.Generic;

namespace BE
{
    public class EntityFamilia : IPermiso
    {
        public string Nombre { get; set; }
        public List<IPermiso> Permisos { get; set; } = new List<IPermiso>();
        public int Id { get; set; }
    }
}
