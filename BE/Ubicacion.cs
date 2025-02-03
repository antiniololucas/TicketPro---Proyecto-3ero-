namespace BE
{
    public class Ubicacion
    {
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public int PlateaGeneral { get; set; }
        public int PlateaPreferencial { get; set; }
        public int Palco { get; set; }
        public int Campo { get; set; }


        public Ubicacion(string nombre, int capacidad, int tienePlateaGeneral, int tienePlateaPreferencial, int tienePalco, int tieneCampo)
        {
            Nombre = nombre;
            Capacidad = capacidad;
            PlateaGeneral = tienePlateaGeneral;
            PlateaPreferencial = tienePlateaPreferencial;
            Palco = tienePalco;
            Campo = tieneCampo;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
