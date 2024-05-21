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

        public List<EntityUser> GetUsers()
        {
            return dataAccess.SelectAllUsers();
        }

        //public EntityUser GetUserByName(string username)
        //{
        //    return dataAccess.SelectByUsername(username);
        //}

        public BusinessResponse<bool> BlockUser(int Id_User)
        {
            bool ok = dataAccess.UpdateBlockUser(Id_User);

            string mensaje = ok ? "Usuario Bloqueado" : "Ocurrió un error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> UnblockUser(int Id_User)
        {
            bool ok = dataAccess.UpdateBlockUser(Id_User);

            string mensaje = ok ? "Operación confirmada" : "Ocurrió un error";

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
            user.Password = CryptoManager.EncryptString(user.Dni.ToString());
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
