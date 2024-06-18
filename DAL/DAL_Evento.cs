using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Evento
    {
        DBConnection conn;
        public DAL_Evento()
        {
            conn = DBConnection.GetInstance();
        }

        public List<EntityEvento> getAll()
        {
            DataTable table = conn.Read("SP_GetEvento", null);
            List<EntityEvento> Eventos = new List<EntityEvento>();
            foreach (DataRow row in table.Rows)
            {
                EntityEvento Evento = SqlMapper.MapEvento(row);
                Eventos.Add(Evento);
            }
            return Eventos;
        }

    }
}
