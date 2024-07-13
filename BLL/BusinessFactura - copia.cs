using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BLL
{
    public class BusinessFactura
    {
        private readonly DAL_Factura dataAcces;

        public BusinessFactura()
        {
            dataAcces = new DAL_Factura();
        }

        public BusinessResponse<List<EntityFactura>> BuscarFacturas()
        {
            List<EntityFactura> facturas = dataAcces.GetAll();
            bool ok = facturas.Count() == 0;
            string Mensaje = ok == true ? "Facturas obtenidas" : "Ocurrió un error";
            return new BusinessResponse<List<EntityFactura>>(ok, facturas, Mensaje);
        }

        public BusinessResponse<bool> RegistrarFactura(EntityFactura factura, List<EntityDetalle_Factura> detalles)
        {
            bool ok = dataAcces.Registrar(factura , detalles);

            string mensaje = ok == true ? "Factura registrada" : "Ocurrió un error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> ModificarFactura(EntityFactura factura, bool isCobranza)
        {   
            bool ok = dataAcces.Update(factura);
            string Mensaje = ok == true ? isCobranza == true ? "Factura cobrada" : "Factura Modificada" : "Ocurrió un error";
            return new BusinessResponse<bool>(ok, false, Mensaje);
        }
    }
}
