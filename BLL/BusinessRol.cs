using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BusinessRol
    {
        private readonly DAL_Rol dataAcces;

        public BusinessRol()
        {
            dataAcces = new DAL_Rol();
        }


        public BusinessResponse<bool> AgregarRol(List<IPermiso> permisos, string nombre)
        {
            bool ok = dataAcces.InsertRol(nombre, permisos);

            string mensaje = ok == true ? "Rol registrado" : "Ocurrió un error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> ModificarRol(EntityRol rol, bool changeName)
        {
            bool ok = ok = dataAcces.UpdateRol(rol);

            string mensaje = ok == true ? "Rol modificado" : "Ocurrió un error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<List<EntityRol>> BuscarRoles()
        {
            List<EntityRol> roles = dataAcces.GetAll();
            bool ok = roles.Count > 0;
            string mensaje = ok is true ? "Lista obtenida" : "Error al traer roles";
            return new BusinessResponse<List<EntityRol>>(ok, roles, mensaje);
        }

        public BusinessResponse<bool> EliminarRol(EntityRol rol)
        {
            bool ok = dataAcces.deleteRol(rol);
            string mensaje = ok is true ? "Rol eliminado" : "Un usuario tiene asignado este rol";
            bool data = false;
            return new BusinessResponse<bool>(ok, data, mensaje);

        }
    }
}
