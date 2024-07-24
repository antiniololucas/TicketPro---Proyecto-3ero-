using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public class BusinessFactura
    {
        private readonly DAL_Factura dataAcces;

        public BusinessFactura()
        {
            dataAcces = new DAL_Factura();
        }
            
        public BusinessResponse<EntityDetalle_Factura> getDetalleFactura(EntityFactura factura)
        {
            var data = dataAcces.getDetalle(factura);
            bool ok = data is null;
            string mensaje = ok is true ? "Obtenido" : "Error";
            return new BusinessResponse<EntityDetalle_Factura> ( ok, data, mensaje );
        }
        public BusinessResponse<List<EntityFactura>> BuscarFacturas()
        {
            List<EntityFactura> facturas = dataAcces.GetAll();
            bool ok = facturas.Count() == 0;
            string Mensaje = ok == true ? "Obtenido" : "Error";
            return new BusinessResponse<List<EntityFactura>>(ok, facturas, Mensaje);
        }

        public BusinessResponse<bool> RegistrarFactura(EntityFactura factura, List<EntityDetalle_Factura> detalles)
        {
            bool ok = dataAcces.Registrar(factura , detalles);

            string mensaje = ok == true ? "Alta" : "Error";

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<bool> ModificarFactura(EntityFactura factura, bool isCobranza)
        {   
            bool ok = dataAcces.Update(factura);
            string Mensaje = ok == true ? isCobranza == true ? "Cobrada" : "Modificada" : "Error";
            return new BusinessResponse<bool>(ok, false, Mensaje);
        }

        public DataTable ObtenerInforme()
        {
            return dataAcces.GetInforme();
        }

    }
}
