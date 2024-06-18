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
    public class DAL_Entrada
    {
        DBConnection conn;

        public DAL_Entrada()
        {
            conn = DBConnection.GetInstance();
        }

        public List<EntityEntrada> Buscar(EntityEvento evento)
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
    }
}
