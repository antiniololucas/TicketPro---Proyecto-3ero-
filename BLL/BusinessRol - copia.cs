using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public BusinessResponse<bool> AgregarRol(List<EntityPermiso> permisos, string nombre)
        {
            throw new NotImplementedException();
        }

    }
}
