using BE;
using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class BusinessEntrada
    {
        private readonly DAL_Entrada dataAccess;

        public BusinessEntrada()
        {
            dataAccess = new DAL_Entrada();
        }

        public BusinessResponse<bool> AgregarEntrada(EntityEntrada item)
        {
            bool ok = dataAccess.InsertEntrada(item);
            string mensaje = ok is true ? "Alta" : "Error";

            return new BusinessResponse<bool>(ok, ok, mensaje);
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
