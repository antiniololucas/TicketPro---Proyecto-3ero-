using BE;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BusinessUser
    {
        private readonly DAL_User dataAccess;

        public BusinessUser()
        {
            dataAccess = new DAL_User();
        }


        public EntityUser GetUserByName(string username)
        {
            return dataAccess.SelectByUsername(username);
        }

        public BusinessResponse<bool> BlockUser(int Id_User)
        {
            bool ok = dataAccess.UpdateBlockUser(Id_User);

            string mensaje = ok ? "Usuario Bloqueado" : "No se pudó bloquear al usuario";
            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> CambiarClave (int id_User, string password)
        {
            password = CryptoManager.EncryptString(password);
            bool ok = dataAccess.UpdatePassword(password, id_User);

            string mensaje = ok ? "Contraseña Modificada\nIngresa sesion con tu nueva contraseña" : "La contraseña no se pudo modificar";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

    }
}
