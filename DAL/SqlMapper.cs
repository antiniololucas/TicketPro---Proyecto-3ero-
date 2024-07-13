using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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

    }
}
