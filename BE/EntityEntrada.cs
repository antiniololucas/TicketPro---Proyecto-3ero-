namespace BE
{
    public class EntityEntrada : Entity
    {
        public int Id_Evento { get; set; }

        public string Tipo { get; set; }

        public double Costo_Unitario { get; set; }

        public int Cantidad_Disponible { get; set; }
    }
}
