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
    public class BusinessAuth
    {
        private readonly DAL_User dataAccess;

        public BusinessAuth()
        {
            dataAccess = new DAL_User();
        }

        public BusinessResponse<EntityUser> VerificarCredenciales(string username, string password)
        {
            EntityUser user = dataAccess.SelectByUsername(username);

            bool ok;
            string mensaje;
            if (user?.IsBlock == true)
            {
                ok = false;
                mensaje = "Usuario Bloqueado";
            }
            else
            {
                ok = user?.Password == CryptoManager.EncryptString(password) && user?.Username == username;
                password = CryptoManager.EncryptString(password);
                mensaje = user?.Username != username ? "Usuario Incorrecto" : !ok ? "Contraseña Incorrecta" : string.Empty;
            }

            return new BusinessResponse<EntityUser>(ok, user, mensaje);
        }
    }
}
