using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Services;
using System.Net.Http.Headers;

namespace BLL
{
    public class BusinessCliente
    {
        private readonly DAL_Cliente dataAccess;

        public BusinessCliente()
        {
            dataAccess = new DAL_Cliente();
        }
        public List<EntityCliente> BuscarClientes()
        {
            return dataAccess.getAll();
        }

        public BusinessResponse<EntityCliente> RegistrarCliente( string dni ,string Nombre, string Apellido, string Mail)
        {

            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(Mail))
            {
                return new BusinessResponse<EntityCliente>(false, null, "Rellene Campos");
            }
            if (!int.TryParse(dni , out int DNI))
            {
                return new BusinessResponse<EntityCliente>(false, null, "El dni tiene un formato incorrecto");
            }
            if (!RegexValidation.IsValidMail(Mail))
            {
                return new BusinessResponse<EntityCliente>(false, null, "El correo tiene un formato incorrecto");
            }

            EntityCliente cliente = new EntityCliente() { DNI = DNI, Apellido = Apellido, Nombre = Nombre, Mail = Mail };
            bool ok = dataAccess.Alta(cliente);
            string mensaje = ok == true ? "Cliente Agregado" : "El dni ya está registrado y le pertenece a un cliente";

            return new BusinessResponse<EntityCliente>(ok, cliente, mensaje);
        }
    }
}
