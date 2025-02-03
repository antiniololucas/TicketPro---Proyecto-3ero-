using System;

namespace BE
{
    public class EntityFactura : Entity
    {
        public int Id_Cliente { get; set; }

        public DateTime Fecha { get; set; }

        public double Monto_Total { get; set; }

        public bool Is_Cobrada { get; set; }

        public int? DNI_Cliente { get; set; }
    }
}
