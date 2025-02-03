namespace BE
{
    public class EntityCliente : Entity
    {
        public int DNI { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Mail { get; set; }

        public bool Is_Planificador { get; set; }
    }
}
