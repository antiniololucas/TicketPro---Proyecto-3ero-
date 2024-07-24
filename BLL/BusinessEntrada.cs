using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BusinessEntrada
    {
        private readonly DAL_Entrada dataAccess;

        public BusinessEntrada()
        {
            dataAccess = new DAL_Entrada();
        }

        public List<EntityEntrada> BuscarEntradas(EntityEvento evento)
        {
            return dataAccess.GetAll(evento);
        }

        public List<EntityEntrada> selectAllEntrads()
        {
            return dataAccess.getAllEntradas();
        }
    }
}
