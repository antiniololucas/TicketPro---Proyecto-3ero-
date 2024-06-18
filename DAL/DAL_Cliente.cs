using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Cliente
    {
        DBConnection conn;
        public DAL_Cliente()
        {
            conn = DBConnection.GetInstance();
        }

        public bool Alta(EntityCliente cliente)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_DNI", SqlDbType.Int) { Value = cliente.DNI },
                new SqlParameter ("@In_Nombre" , SqlDbType.NVarChar){ Value = cliente.Nombre},
                new SqlParameter("@In_Apellido" , SqlDbType.NVarChar) { Value = cliente.Apellido },
                new SqlParameter("@In_Mail" , SqlDbType.NVarChar) { Value = cliente.Mail},
            };

            return conn.Write("SP_InsertCliente", parameters);
        }

        public List<EntityCliente> getAll()
        {
            DataTable table = conn.Read("SP_GetCliente", null);
            List<EntityCliente> users = new List<EntityCliente>();
            foreach (DataRow row in table.Rows)
            {
                EntityCliente user = SqlMapper.MapCliente(row);
                users.Add(user);
            }
            return users;
        }
    }
}
