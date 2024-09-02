using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Diagnostics;

namespace DAL
{
    public class DAL_EventoBitacora
    {
        DBConnection conn;

        public DAL_EventoBitacora()
        {
            conn = DBConnection.GetInstance();
        }

        public bool InsertEventoBitacora(int usuario, string modulo, string evento, int criticidad)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Login", SqlDbType.Int) { Value = usuario },
                new SqlParameter ("@In_Modulo" , SqlDbType.NVarChar){ Value = modulo},
                new SqlParameter("@In_Evento" , SqlDbType.NVarChar) { Value = evento },
                new SqlParameter("@In_Criticidad" , SqlDbType.Int) { Value = criticidad},
            };

            return conn.Write("SP_InsertEventoBitacora", parameters);
        }

        public List<EntityEventoBitacora> GetEventosBitacora(int usuario , DateTime fecha_inicio , DateTime fecha_fin , string modulo, string evento , int criticidad)
        {
            List<EntityEventoBitacora> eventos_bitacora = new List<EntityEventoBitacora>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                usuario != 0 ? new SqlParameter("@In_IdUser", SqlDbType.Int) { Value = usuario } : null,
                fecha_inicio != (DateTime.Now.AddDays(-3)) ?   new SqlParameter("@In_FechaInicio", SqlDbType.DateTime) { Value = fecha_inicio } : null,
                fecha_fin != DateTime.Now ? new SqlParameter("@In_FechaFin", SqlDbType.DateTime) { Value = fecha_fin } : null,
                new SqlParameter("@In_Modulo", SqlDbType.NVarChar) { Value = modulo },
                new SqlParameter("@In_Evento", SqlDbType.NVarChar) { Value = evento },
                criticidad != 0 ? new SqlParameter("@In_Criticidad", SqlDbType.Int) { Value = criticidad } : null,
            };

            // Filtrar elementos nulos
            parameters = parameters.Where(p => p != null).ToArray();

            DataTable data = conn.Read("SP_SelectEventoBitacora", parameters);

            foreach (DataRow row in data.Rows)
            {
                eventos_bitacora.Add(SqlMapper.MapEventoBitacora(row));
            }
            return eventos_bitacora;
        }

        public List<string> getTiposEvento()
        {
            List<string> tipos_eventos_bitacora = new List<string>();

            DataTable data = conn.Read("SP_SelectTiposEventosBitacora", null);

            foreach (DataRow row in data.Rows)
            {
                tipos_eventos_bitacora.Add(row["Evento"].ToString());
            }
            return tipos_eventos_bitacora;
        }

        public List<string> getModulos()
        {
            List<string> modulos_eventos_bitacora = new List<string>();

            DataTable data = conn.Read("SP_SelectModulos", null);

            foreach (DataRow row in data.Rows)
            {
                modulos_eventos_bitacora.Add(row["Modulo"].ToString());
            }
            return modulos_eventos_bitacora;
        }
    }
}
