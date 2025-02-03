using BE;
using System;
using System.Data;

namespace DAL
{
    internal static class SqlMapper
    {
        public static EntityUser MapUser(DataRow row) => new EntityUser()
        {
            Id = Convert.ToInt32(row["ID"]),
            Dni = Convert.ToInt32(row["Dni"]),
            Nombre = row["Nombre"].ToString(),
            Apellido = row["Apellido"].ToString(),
            Username = row["Username"].ToString(),
            Password = row["Password"].ToString(),
            IsBlock = Convert.ToBoolean(row["IsBlock"].ToString()),
            Rol = new EntityRol()
            {
                Id = Convert.ToInt32(row["Id_Rol"]),
                Nombre = row["NombreRol"].ToString()
            },
        };

        public static EntityRol MapRol(DataRow row) => new EntityRol()
        {
            Id = Convert.ToInt32(row["Id_Rol"]),
            Nombre = row["Nombre"].ToString(),
        };

        public static EntityEvento MapEvento(DataRow row) => new EntityEvento()
        {
            Id = Convert.ToInt32(row["ID"]),
            Nombre = row["Nombre"].ToString(),
            Descripcion = row["Descripcion"].ToString(),
            Artista = row["Artista"].ToString(),
            Ubicacion = row["Ubicacion"].ToString(),
            Fecha = DateTime.Parse(row["Fecha"].ToString()),
            Horario = TimeSpan.Parse(row["Horario"].ToString()),
            Imagen = (byte[])row["Imagen"],
            Id_Planificador = int.Parse(row["Id_Planificador"].ToString()),
            Is_Paga = bool.Parse(row["Is_Paga"].ToString()),
            PublicoObjetivo = row["PublicoObjetivo"].ToString()
        };

        public static EntityEntrada MapEntrada(DataRow row) => new EntityEntrada()
        {
            Id = Convert.ToInt32(row["ID"].ToString()),
            Id_Evento = Convert.ToInt32(row["Id_Evento"].ToString()),
            Tipo = row["Tipo"].ToString(),
            Costo_Unitario = double.Parse(row["Costo_Unitario"].ToString()),
            Cantidad_Disponible = Convert.ToInt32(row["Cantidad_Disponible"].ToString()),
        };

        public static EntityCliente MapCliente(DataRow row) => new EntityCliente()
        {
            Id = Convert.ToInt32(row["ID"].ToString()),
            DNI = Convert.ToInt32(row["DNI"].ToString()),
            Nombre = row["Nombre"].ToString(),
            Apellido = row["Apellido"].ToString(),
            Mail = row["Mail"].ToString(),
            Is_Planificador = bool.Parse(row["Is_Planificador"].ToString())
        };

        internal static EntityFactura MapFactura(DataRow row) => new EntityFactura()
        {
            Id = Convert.ToInt32(row["ID"].ToString()),
            Id_Cliente = Convert.ToInt32(row["Id_Cliente"].ToString()),
            Fecha = DateTime.Parse(row["Fecha"].ToString()),
            Monto_Total = double.Parse(row["Monto_Total"].ToString()),
            Is_Cobrada = bool.Parse(row["Is_Cobrada"].ToString()),
        };


        internal static EntityPermiso MapPermiso(DataRow row) => new EntityPermiso()
        {
            Nombre = row["Nombre"].ToString(),
            Id = Convert.ToInt32(row["Id_Permiso"].ToString()),
        };

        internal static EntityFamilia MapFamilia(DataRow row) => new EntityFamilia()
        {
            Nombre = row["Nombre"].ToString(),
            Id = Convert.ToInt32(row["Id_Permiso"].ToString()),
        };

        internal static EntityDetalle_Factura MapDetalleFactura(DataRow row) => new EntityDetalle_Factura()
        {
            Id_Detalle = Convert.ToInt32(row["Id_detalle"].ToString()),
            Id_Factura = Convert.ToInt32(row["Id_Factura"].ToString()),
            Id_Entrada = Convert.ToInt32(row["Id_Entrada"].ToString()),
            Cantidad_Entradas = Convert.ToInt32(row["Cantidad_Entradas"].ToString()),
            Costo_parcial = double.Parse(row["Costo_Parcial"].ToString())
        };

        public static EntityIdioma MapIdioma(DataRow row) => new EntityIdioma()
        {
            Id = Convert.ToInt32(row["Id_Idioma"]),
            Idioma = row["Idioma"].ToString()
        };

        public static EntityEventoBitacora MapEventoBitacora(DataRow row) => new EntityEventoBitacora()
        {
            Id = Convert.ToInt32(row["Id"]),
            Fecha = Convert.ToDateTime(row["Fecha"]),
            Modulo = row["Modulo"].ToString(),
            Evento = row["Evento"].ToString(),
            Criticidad = Convert.ToInt32(row["Criticidad"]),
            Login = new EntityUser()
            {
                Id = Convert.ToInt32(row["Id_User"]),
                Username = row["Username"].ToString(),
                Password = row["Password"].ToString(),
                IsBlock = Convert.ToBoolean(row["IsBlock"]),
                Dni = Convert.ToInt32(row["DNI"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString()
            }
        };

        public static EntityEvento_C MapEvento_C(DataRow row) => new EntityEvento_C()
        {
            Id = Convert.ToInt32(row["ID"]),
            Nombre = row["Nombre"].ToString(),
            Descripcion = row["Descripcion"].ToString(),
            Artista = row["Artista"].ToString(),
            Ubicacion = row["Ubicacion"].ToString(),
            Fecha = DateTime.Parse(row["Fecha"].ToString()),
            Horario = TimeSpan.Parse(row["Horario"].ToString()),
            Imagen = (byte[])row["Imagen"],
            Id_Planificador = int.Parse(row["Id_Planificador"].ToString()),
            Is_Paga = bool.Parse(row["Is_Paga"].ToString()),
            Act = bool.Parse(row["Act"].ToString()),
            Fecha_Modificacion = DateTime.Parse(row["Fecha_Modificacion"].ToString()),
            PublicoObjetivo = row["PublicoObjetivo"].ToString()
        };

        public static EntityDigitoVerificador MapDigitoVerificador(DataRow row) => new EntityDigitoVerificador()
        {
            Id = Convert.ToInt32(row["ID"]),
            DVH = row["DVH"].ToString(),
            DVV = row["DVV"].ToString()
        };
    }
}
