using BE;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_DigitoVerificador
    {
        DBConnection conn;
        public DAL_DigitoVerificador()
        {
            conn = DBConnection.GetInstance();
        }

        public EntityDigitoVerificador GetDigitoVerificador()
        {

            DataTable data = conn.Read("SP_GetDV", null);

            return SqlMapper.MapDigitoVerificador(data.Rows[data.Rows.Count - 1]);
        }

        public DataTable CountDigitoVerificador(string tableName)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_NameTable", SqlDbType.NVarChar){ Value = tableName}
            };
            return conn.Read("SP_GetTable", parameters);
        }

        public bool UpdateDigitoVerificador(EntityDigitoVerificador DV)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_DVV" ,  SqlDbType.NVarChar){Value = DV.DVV},
                new SqlParameter("@iN_dvh" ,  SqlDbType.NVarChar){Value = DV.DVH},
            };

            return conn.Write("SP_UpdateDV", parameters);
        }
    }
}
