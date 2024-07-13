using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Services;
using System.Net.Http.Headers;
using System.Net;

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
            return dataAccess.GetAll();
        }

        public BusinessResponse<bool> ModificarCliente(int id, string dni, string Nombre, string Apellido, string Mail)
        {
            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(Mail))
            {
                return new BusinessResponse<bool>(false, false, "Rellene Campos");
            }
            if (!int.TryParse(dni, out int DNI))
            {
                return new BusinessResponse<bool>(false, false, "El dni tiene un formato incorrecto");
            }
            if (!RegexValidation.IsValidMail(Mail))
            {
                return new BusinessResponse<bool>(false, false, "El correo tiene un formato incorrecto");
            }
            Mail = CryptoManager.ReversibleEncrypt(Mail);

            EntityCliente cliente = new EntityCliente() {Id = id, DNI = DNI, Apellido = Apellido, Nombre = Nombre, Mail = Mail };

            bool ok = dataAccess.UpdateCliente(cliente);
            string mensaje = ok == true ? "Cliente modificado" : "Ocurrió un error";

            return new BusinessResponse<bool>(ok, false, mensaje);
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
            Mail = CryptoManager.ReversibleEncrypt(Mail);

            EntityCliente cliente = new EntityCliente() { DNI = DNI, Apellido = Apellido, Nombre = Nombre, Mail = Mail };

            bool ok = dataAccess.Insert_Cliente(cliente);
            string mensaje = ok == true ? "Cliente Registrado" : "El dni ya está registrado y le pertenece a un cliente";

            return new BusinessResponse<EntityCliente>(ok, cliente, mensaje);
        }
    }
}
