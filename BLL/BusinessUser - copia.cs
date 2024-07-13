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
        private readonly BusinessRol bussinesRol;
        public BusinessUser()
        {
            dataAccess = new DAL_User();
            bussinesRol = new BusinessRol();
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
                mensaje = "Usuario Bloqueado";
            }
            else
            {
                ok = user?.Password == CryptoManager.EncryptString(password) && user?.Username == username;
                string pass = CryptoManager.EncryptString(password);
                mensaje = user?.Username != username ? "Usuario Incorrecto" : !ok ? "Contraseña Incorrecta" : string.Empty;
            }

            return new BusinessResponse<EntityUser>(ok, user, mensaje);
        }

        public BusinessResponse<bool> ChangeBlockUser(EntityUser user)
        {
            bool ok = dataAccess.UpdateBlockUser(user.Id);
            string mensaje;

            if (!user.IsBlock)
            {
                mensaje = ok ? "Usuario Bloqueado" : "Ocurrió un error";
            }
            else
            {
                mensaje = ok ? "Usuario Desbloqueado" : "Ocurrió un error";
                //Resetea clave
                dataAccess.UpdatePassword(CryptoManager.EncryptString(user.Dni.ToString() + user.Apellido.ToString()), user.Id);
            }
            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> CambiarClave (int id_User, string password)
        {
            password = CryptoManager.EncryptString(password);
            bool ok = dataAccess.UpdatePassword(password, id_User);

            string mensaje = ok ? "Contraseña Modificada\nIngresa sesion con tu nueva contraseña" : "La contraseña no se pudo modificar";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> EliminarUsuario (int id_user)
        {
            bool ok = dataAccess.DeleteUser(id_user);

            string mensaje = ok ? "Usuario eliminado" : "Ocurrió un error";

            return new BusinessResponse<bool>(ok , false, mensaje);
        }

        public BusinessResponse<bool> AgregarUsuario ( EntityUser user)
        {
            user.Password = CryptoManager.EncryptString(user.Dni.ToString() + user.Apellido.ToString());
            user.Username = user.Nombre + user.Apellido;
            bool ok = dataAccess.InsertUser(user);

            string mensaje = ok ? "Se agregó el usuario" : "Ocurrió un error, verifique campos y pruebe agregando un segundo nombre";

            return new BusinessResponse<bool> ( ok, false, mensaje );
        }

        public BusinessResponse<bool> ModificarUsuario (EntityUser user) 
        {
            bool ok = dataAccess.UpdateUser(user);

            string mensaje = ok ? "Se modificó el usuario" : "Ocurrió un error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

    }
}
