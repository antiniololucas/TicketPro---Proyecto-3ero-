namespace BE
{
    public class EntityUser : Entity
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsBlock { get; set; }
        public EntityRol Rol { get; set; }

        public override string ToString()
        {
            return Username;
        }
    }


}
