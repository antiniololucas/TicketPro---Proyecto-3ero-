using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BusinessEvento
    {
        private readonly DAL_Evento dataAccess;

        public BusinessEvento()
        {
           dataAccess = new DAL_Evento();
        }

        public List<EntityEvento> BuscarEventos()
        {
            return dataAccess.getAll();
        }

    }
}
