using System;

namespace BE
{
    public class EntityEvento : Entity
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Artista { get; set; }

        public string Ubicacion { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Horario { get; set; }

        public byte[] Imagen { get; set; }

        public int Id_Planificador { get; set; }

        public bool Is_Paga { get; set; }

        public string PublicoObjetivo { get; set; }
    }
}
