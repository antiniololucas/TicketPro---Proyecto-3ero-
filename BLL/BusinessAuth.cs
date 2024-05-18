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
        private readonly DataAccessUser dataAccess;

        public BusinessAuth()
        {
            dataAccess = new DataAccessUser();
        }

        public BusinessResponse<EntityUser> VerificarCredenciales(string username, string password)
        {
            EntityUser user = dataAccess.SelectByUsername(username);
            bool ok = user?.Password == CryptoManager.EncryptString(password) && user?.Username == username;

            string mensaje = user?.Username != username ? "Usuario Incorrecto" : !ok ? "Contraseña Incorrecta" : string.Empty;

            return new BusinessResponse<EntityUser>(ok, user, mensaje);
        }
    }
}
