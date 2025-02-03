using BE;
using DAL;
using Services;
using System.Collections.Generic;

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
                return new BusinessResponse<bool>(false, false, "Campos_Incompletos");
            }
            if (!int.TryParse(dni, out int DNI))
            {
                return new BusinessResponse<bool>(false, false, "FormatoIncorrecto");
            }
            if (!RegexValidation.IsValidMail(Mail))
            {
                return new BusinessResponse<bool>(false, false, "FormatoIncorrecto");
            }
            Mail = CryptoManager.ReversibleEncrypt(Mail);

            EntityCliente cliente = new EntityCliente() { Id = id, DNI = DNI, Apellido = Apellido, Nombre = Nombre, Mail = Mail };

            bool ok = dataAccess.UpdateCliente(cliente);
            string mensaje = ok == true ? "Modificada" : "Error"; //Nombre del control

            return new BusinessResponse<bool>(ok, false, mensaje);
        }

        public BusinessResponse<EntityCliente> RegistrarCliente(string dni, string Nombre, string Apellido, string Mail, bool Is_Planificador)
        {

            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(Mail))
            {
                return new BusinessResponse<EntityCliente>(false, null, "Campos_Incompletos");
            }
            if (!int.TryParse(dni, out int DNI))
            {
                return new BusinessResponse<EntityCliente>(false, null, "FormatoIncorrecto");
            }
            if (!RegexValidation.IsValidMail(Mail))
            {
                return new BusinessResponse<EntityCliente>(false, null, "FormatoIncorrecto");
            }
            Mail = CryptoManager.ReversibleEncrypt(Mail);

            EntityCliente cliente = new EntityCliente() { DNI = DNI, Apellido = Apellido, Nombre = Nombre, Mail = Mail, Is_Planificador = Is_Planificador };

            bool ok = dataAccess.Insert_Cliente(cliente);
            string mensaje = ok == true ? "Alta" : "Error";

            return new BusinessResponse<EntityCliente>(ok, cliente, mensaje);
        }
    }
}
