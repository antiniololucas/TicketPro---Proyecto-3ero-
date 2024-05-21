using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_User
    {
        DBConnection conn;
        public DAL_User()
        {
            conn = DBConnection.GetInstance();
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

    }
}
