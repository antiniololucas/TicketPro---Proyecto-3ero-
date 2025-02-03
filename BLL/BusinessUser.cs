using BE;
using DAL;
using Services;
using System.Collections.Generic;

namespace BLL
{
    public class BusinessUser
    {
        private readonly DAL_User dataAccess;
        public BusinessUser()
        {
            dataAccess = new DAL_User();
        }

        public List<EntityUser> GetUsers()
        {
            return dataAccess.SelectAllUsers();
        }

        public BusinessResponse<EntityUser> VerificarCredenciales(string username, string password)
        {
            EntityUser user = dataAccess.SelectByUsername(username);

            bool ok;
            string mensaje;
            if (user?.IsBlock == true)
            {
                ok = false;
                mensaje = "UsuarioBloqueado";
            }
            else
            {
                ok = user?.Password == CryptoManager.EncryptString(password) && user?.Username == username;
                string pass = CryptoManager.EncryptString(password);
                mensaje = user?.Username != username ? "UsuarioIncorrecto" : !ok ? "ContraseñaIncorrecta" : string.Empty;
            }

            return new BusinessResponse<EntityUser>(ok, user, mensaje);
        }

        public BusinessResponse<bool> ChangeBlockUser(EntityUser user)
        {
            bool ok = dataAccess.UpdateBlockUser(user.Id);
            string mensaje;

            if (!user.IsBlock)
            {
                mensaje = ok ? "UsuarioBloqueado" : "Error";
            }
            else
            {
                mensaje = ok ? "UsuarioDesbloqueado" : "Error";
                //Resetea clave
                dataAccess.UpdatePassword(CryptoManager.EncryptString(user.Dni.ToString() + user.Apellido.ToString()), user.Id);
            }
            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> CambiarClave(int id_User, string password)
        {
            password = CryptoManager.EncryptString(password);
            bool ok = dataAccess.UpdatePassword(password, id_User);

            string mensaje = ok ? "UpdatePassword" : "Error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> EliminarUsuario(int id_user)
        {
            bool ok = dataAccess.DeleteUser(id_user);

            string mensaje = ok ? "Borrada" : "Error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> AgregarUsuario(EntityUser user)
        {
            user.Password = CryptoManager.EncryptString(user.Dni.ToString() + user.Apellido.ToString());
            user.Username = user.Nombre + user.Apellido;
            bool ok = dataAccess.InsertUser(user);

            string mensaje = ok ? "Alta" : "Error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> ModificarUsuario(EntityUser user)
        {
            bool ok = dataAccess.UpdateUser(user);

            string mensaje = ok ? "Modificada" : "Error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

    }
}
