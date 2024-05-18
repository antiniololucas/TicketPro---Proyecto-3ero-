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
    public class DataAccessUser
    {
        DBConnection conn;
        public DataAccessUser()
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
    }
}
