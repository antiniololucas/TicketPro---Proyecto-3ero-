using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BusinessEventoBitacora
    {
        private readonly DAL_EventoBitacora dataAccess;

        public BusinessEventoBitacora()
        {
            dataAccess = new DAL_EventoBitacora();
        }

        public BusinessResponse<bool> guardarEventoBitacora(int usuario,string modulo, string evento, int criticidad)
        {
            bool ok = dataAccess.InsertEventoBitacora( usuario,  modulo,  evento,  criticidad);
            string mensaje = ok is true ? "Alta" : "Error";

            return new BusinessResponse<bool>(ok, true , mensaje);
        }

        public BusinessResponse<List<EntityEventoBitacora>> buscarEventosBitacora(int usuario , DateTime fecha_inicio , DateTime fecha_fin,  int criticidad , string modulo = null, string evento = null)
        {
            List<EntityEventoBitacora> eventos_bitacoras = dataAccess.GetEventosBitacora(usuario, fecha_inicio, fecha_fin, modulo, evento, criticidad);
            bool ok = eventos_bitacoras.Count > 0;
            string mensaje = ok is true ? "Obtenido" : "Error";
            
            return new BusinessResponse<List<EntityEventoBitacora>>(ok , eventos_bitacoras, mensaje);
        }

        public List<string> selectTiposEvento()
        {
            return dataAccess.getTiposEvento();
        }

        public List<string> selectModulos()
        {
            return dataAccess.getModulos();
        }
    }
}
