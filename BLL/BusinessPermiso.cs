using BE;
using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class BusinessPermiso
    {

        private readonly DAL_Permiso dataAccess;
        public BusinessPermiso()
        {
            dataAccess = new DAL_Permiso();
        }

        public BusinessResponse<bool> EliminarFamilia(IPermiso permisoActual)
        {
            bool ok = dataAccess.deleteFamilia(permisoActual);
            string mensaje = ok is true ? "Borrada" : "Error";
            bool data = false;
            return new BusinessResponse<bool>(ok, data, mensaje);
        }

        public BusinessResponse<List<IPermiso>> getPermisosPorUser(EntityUser user)
        {
            List<IPermiso> list = dataAccess.GetPorUser(user);
            bool ok = list.Count > 0;
            string mensaje = ok is true ? "Obtenido" : "Error";

            return new BusinessResponse<List<IPermiso>>(ok, list, mensaje);
        }

        public BusinessResponse<bool> ModificarFamilia(EntityFamilia familiaActual, bool changeName)
        {
            bool ok = dataAccess.UpdateFamilia(familiaActual, changeName);
            string mensaje = ok is true ? "Modificada" : "Error";
            bool data = false;

            return new BusinessResponse<bool>(ok, data, mensaje);
        }

        public BusinessResponse<List<IPermiso>> ObtenerPermisos()
        {
            List<IPermiso> list = dataAccess.GetAll();
            bool ok = list.Count > 0;
            string mensaje = ok is true ? "Obtenido" : "Error";

            return new BusinessResponse<List<IPermiso>>(ok, list, mensaje);
        }

        public BusinessResponse<bool> RegistrarFamilia(List<IPermiso> permisosFamiliaElegida, string nombre)
        {
            bool ok = dataAccess.InsertFamilia(permisosFamiliaElegida, nombre);
            string mensaje = ok is true ? "Alta" : "Error";
            var data = false;
            return new BusinessResponse<bool>(ok, data, mensaje);
        }
    }

}

