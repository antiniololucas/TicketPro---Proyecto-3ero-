using BE;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_User
    {
        DBConnection conn;
        public DAL_User()
        {
            conn = DBConnection.GetInstance();
        }

        public bool DeleteUser(int id_user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Id", SqlDbType.Int) { Value = id_user }
            };

            return conn.Write("SP_DeleteUser", parameters);
        }

        public bool InsertUser(EntityUser user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_DNI", SqlDbType.Int) { Value = user.Dni },
                new SqlParameter("@In_Apellido" , SqlDbType.NVarChar) { Value = user.Apellido },
                new SqlParameter ("@In_Nombre" , SqlDbType.NVarChar){ Value = user.Nombre},
                new SqlParameter("@In_Username" , SqlDbType.NVarChar) { Value = user.Username},
                new SqlParameter("@In_Password" , SqlDbType.NVarChar) {Value = user.Password},
                new SqlParameter("@In_Rol" , SqlDbType.Int) {Value = user.Rol.Id }
            };

            return conn.Write("SP_InsertUser", parameters);
        }

        public List<EntityUser> SelectAllUsers()
        {
            DataTable table = conn.Read("SP_GetUsers", null);
            List<EntityUser> users = new List<EntityUser>();
            foreach (DataRow row in table.Rows)
            {
                EntityUser user = SqlMapper.MapUser(row);
                users.Add(user);
            }
            return users;
        }

        public EntityUser SelectByUsername(string username)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_username", SqlDbType.NVarChar){ Value = username }
            };

            DataTable data = conn.Read("SP_GetUser", parameters);

            return data.Rows.Count == 0 ? null : SqlMapper.MapUser(data.Rows[0]);
        }

        public bool UpdateBlockUser(int id_user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Id", SqlDbType.Int) { Value = id_user }
            };

            return conn.Write("SP_BlockUser", parameters);
        }

        public bool UpdatePassword(string password, int id_user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Id", SqlDbType.Int) { Value = id_user } ,
                new SqlParameter("@In_NewPassword", SqlDbType.NVarChar) { Value = password }
            };

            return conn.Write("SP_UpdatePassword", parameters);
        }

        public bool UpdateUser(EntityUser user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Id", SqlDbType.Int) { Value = user.Id },
                new SqlParameter("@In_DNI", SqlDbType.Int) { Value = user.Dni },
                new SqlParameter ("@In_Nombre" , SqlDbType.NVarChar){ Value = user.Nombre},
                new SqlParameter("@In_Apellido" , SqlDbType.NVarChar) { Value = user.Apellido },
                new SqlParameter("@In_Rol" , SqlDbType.Int) {Value = user.Rol.Id }
            };

            return conn.Write("SP_UpdateUser", parameters);
        }
    }
}
