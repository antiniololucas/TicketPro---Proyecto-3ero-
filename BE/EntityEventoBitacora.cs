using System;

namespace BE
{
    public class EntityEventoBitacora : Entity
    {
        public EntityUser Login { get; set; }

        public DateTime Fecha { get; set; }

        public string Modulo { get; set; }

        public string Evento { get; set; }

        public int Criticidad { get; set; }
    }
}
