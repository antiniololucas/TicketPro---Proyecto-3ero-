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

        public BusinessResponse<List<IPermiso>> getPermisosPorUser(EntityUser user)
        {
            List<IPermiso> list = dataAccess.GetPorUser(user);
            bool ok = list.Count > 0;
            string mensaje = ok is true ? "Permisos obtenidos" : "Permisos no encontrados";

            return new BusinessResponse<List<IPermiso>>(ok, list, mensaje);
        }

        public BusinessResponse<List<IPermiso>> ObtenerPermisos()
        {
            List<IPermiso> list = dataAccess.GetAll();
            bool ok = list.Count > 0;
            string mensaje = ok is true ? "Permisos obtenidos" : "Permisos no encontrados";

            return new BusinessResponse<List<IPermiso>>(ok, list, mensaje);
        }

    }

}

