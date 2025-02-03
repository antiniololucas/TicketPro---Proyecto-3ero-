using System.Collections.Generic;

namespace BE
{
    public static class Ubicaciones
    {
        public static List<Ubicacion> Ubicaciones_General { get; set; } = new List<Ubicacion>
    {
        // bool tienePlateaGeneral , bool tienePlateaPreferencial, bool tienePalco, bool tieneCampo
        new Ubicacion("Estado River Plate - Buenos Aires", 84000, 20000, 14000, 10000, 40000),
        new Ubicacion("Movistar Arena - Buenos Aires", 15000, 5000 , 0, 2000, 8000),
        new Ubicacion("Estadio Velez Sarsfield - Buenos Aires", 49500, 15500, 0 , 0, 34000)
    };
    }

}
