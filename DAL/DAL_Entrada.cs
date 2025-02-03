using BE;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_Entrada
    {
        DBConnection conn;

        public DAL_Entrada()
        {
            conn = DBConnection.GetInstance();
        }

        public List<EntityEntrada> GetAll(EntityEvento evento)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Id_Evento", SqlDbType.Int) { Value = evento.Id }
            };
            DataTable table = conn.Read("SP_GetEntrada", parameters);
            List<EntityEntrada> Entradas = new List<EntityEntrada>();
            foreach (DataRow row in table.Rows)
            {
                EntityEntrada entrada = SqlMapper.MapEntrada(row);
                Entradas.Add(entrada);
            }
            return Entradas;
        }

        public List<EntityEntrada> getAllEntradas()
        {

            DataTable table = conn.Read("SP_GetAllEntradas", null);
            List<EntityEntrada> Entradas = new List<EntityEntrada>();
            foreach (DataRow row in table.Rows)
            {
                EntityEntrada entrada = SqlMapper.MapEntrada(row);
                Entradas.Add(entrada);
            }
            return Entradas;
        }

        public bool InsertEntrada(EntityEntrada item)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Id_Evento", SqlDbType.Int) { Value = item.Id_Evento },
                new SqlParameter("@In_Tipo", SqlDbType.NVarChar) { Value = item.Tipo },
                new SqlParameter("@In_Costo", SqlDbType.Money) { Value = item.Costo_Unitario } ,
                new SqlParameter("@In_Cantidad", SqlDbType.Int) { Value = item.Cantidad_Disponible }
            };

            return conn.Write("SP_InsertEntrada", parameters);
        }
    }
}
