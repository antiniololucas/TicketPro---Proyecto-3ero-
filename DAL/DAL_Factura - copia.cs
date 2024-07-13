using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Factura
    {
        DBConnection conn;
        public DAL_Factura()
        {
            conn = DBConnection.GetInstance();
        }

        public List<EntityFactura> GetAll()
        {
            DataTable table = conn.Read("SP_GetFacturas", null);
            List<EntityFactura> facturas = new List<EntityFactura>();
            foreach (DataRow row in table.Rows)
            {
                EntityFactura fac = SqlMapper.MapFactura(row);
                facturas.Add(fac);
            }
            return facturas;
        }

        public bool Registrar(EntityFactura factura, List<EntityDetalle_Factura> detalles)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Id_Cliente", SqlDbType.Int) { Value = factura.Id_Cliente },
                new SqlParameter ("@In_Fecha" , SqlDbType.Date){ Value = factura.Fecha},
                new SqlParameter("@In_Monto_Total" , SqlDbType.Money) { Value = factura.Monto_Total },
            };

            var result = conn.WriteWithReturn("SP_InsertFactura", parameters);
            int newId = Convert.ToInt32(result);

            if (result is null) { return false; }

            foreach (var detalle in detalles)
            {
                InsertarDetalleFactura(newId, detalle);
            }
       
            return true;
        }

        public bool Update(EntityFactura factura)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                 new SqlParameter("@In_Id", SqlDbType.Int) { Value = factura.Id },
                 new SqlParameter("@In_Id_Cliente", SqlDbType.Int) { Value = factura.Id_Cliente },
                 new SqlParameter("@In_Fecha", SqlDbType.Date) { Value = factura.Fecha },
                 new SqlParameter("@In_Monto_Total", SqlDbType.Money) { Value = factura.Monto_Total },
                 new SqlParameter("@In_Is_Cobrada", SqlDbType.Bit) { Value = factura.Is_Cobrada },
           };

           return conn.Write("SP_UpdateFactura", parameters);
        }

        //Seguir con detalle
        private void InsertarDetalleFactura(int facturaId, EntityDetalle_Factura detalle)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@In_Id_Factura", SqlDbType.Int) { Value = facturaId },
                 new SqlParameter("@In_Id_Entrada", SqlDbType.Int) { Value = detalle.Id_Entrada },
                 new SqlParameter("@In_Cantidad_Entradas", SqlDbType.Int) { Value = detalle.Cantidad_Entradas },
                 new SqlParameter("@In_Costo_Parcial", SqlDbType.Money) { Value = detalle.Costo_parcial },
            };

            conn.Write("SP_InsertDetalleFactura", parameters);
        }
    }
}
