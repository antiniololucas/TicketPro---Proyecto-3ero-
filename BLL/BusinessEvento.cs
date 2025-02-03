using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class BusinessEvento
    {
        private readonly DAL_Evento dataAccess;

        public BusinessEvento()
        {
            dataAccess = new DAL_Evento();
        }

        public double BuscarMonto(int id)
        {
            return dataAccess.GetMontoTotal(id);
        }

        public BusinessResponse<int> AgregarEvento(EntityEvento evento_Actual, EntityCliente asociado_actual)
        {
            int number = dataAccess.InsertEvento(evento_Actual, asociado_actual);
            bool ok = number > 0;
            string mensaje = ok ? "Alta" : "Error";
            return new BusinessResponse<int>(ok, number, mensaje);
        }

        public List<EntityEvento> BuscarEventos()
        {
            return dataAccess.getAll();
        }

        public List<EntityEvento_C> BuscarEventos_C()
        {
            return dataAccess.getAll_C();
        }

        public BusinessResponse<bool> ModificarEvento(EntityEvento evento)
        {
            bool ok = dataAccess.UpdateEvento(evento);
            string mensaje = ok is true ? "EventoModificado" : "Error";
            return new BusinessResponse<bool>(ok, ok, mensaje);
        }

        public DataTable ObtenerInforme()
        {
            return dataAccess.getInforme();
        }

        public List<string> BuscarGustosMusicales()
        {
            return dataAccess.GetGustosMusicales();
        }

        public void AgregarGustoCliente(int id, string publicoObjetivo)
        {
            dataAccess.InsertGustoCliente(id, publicoObjetivo);
        }

        public List<int> EnviarPromocionMail(EntityEvento evento_Actual)
        {
            return dataAccess.GetClientesXGusto(evento_Actual.PublicoObjetivo);
        }
    }
}
