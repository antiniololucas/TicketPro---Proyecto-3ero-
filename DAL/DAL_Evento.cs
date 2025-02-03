using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;

namespace DAL
{
    public class DAL_Evento
    {
        DBConnection conn;
        public DAL_Evento()
        {
            conn = DBConnection.GetInstance();
        }

        public List<EntityEvento_C> getAll_C()
        {
            DataTable table = conn.Read("SP_GetEventos_C", null);
            List<EntityEvento_C> Eventos = new List<EntityEvento_C>();
            foreach (DataRow row in table.Rows)
            {
                EntityEvento_C Evento = SqlMapper.MapEvento_C(row);
                Eventos.Add(Evento);
            }
            return Eventos;
        }
        public List<EntityEvento> getAll()
        {
            DataTable table = conn.Read("SP_GetEvento", null);
            List<EntityEvento> Eventos = new List<EntityEvento>();
            foreach (DataRow row in table.Rows)
            {
                EntityEvento Evento = SqlMapper.MapEvento(row);
                Eventos.Add(Evento);
            }
            return Eventos;
        }


        public bool UpdateEvento(EntityEvento evento)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Id", SqlDbType.Int) { Value = evento.Id },
                new SqlParameter ("@In_Nombre" , SqlDbType.NVarChar){ Value = evento.Nombre },
                new SqlParameter("@In_Artista" , SqlDbType.NVarChar) { Value = evento.Artista },
                new SqlParameter("@In_Descripcion" , SqlDbType.NVarChar) {Value = evento.Descripcion},
                new SqlParameter("@In_Ubicacion" , SqlDbType.NVarChar) {Value = evento.Ubicacion},
                new SqlParameter("@In_Imagen" , SqlDbType.VarBinary) {Value = evento.Imagen},
                new SqlParameter("@In_Horario" , SqlDbType.Time) {Value = evento.Horario},
                new SqlParameter("@In_Fecha" , SqlDbType.Date) {Value = evento.Fecha},
                new SqlParameter("@In_Id_Planificador" , SqlDbType.Int) {Value = evento.Id_Planificador},
                new SqlParameter("@Is_Paga" , SqlDbType.Bit) {Value = evento.Is_Paga},
                new SqlParameter("@In_PublicoObjetivo" , SqlDbType.NVarChar) {Value = evento.PublicoObjetivo},
            };

            return conn.Write("SP_UpdateEvento", parameters);
        }

        public int InsertEvento(EntityEvento evento, EntityCliente asociado_actual)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter ("@In_Nombre" , SqlDbType.NVarChar){ Value = evento.Nombre },
                new SqlParameter("@In_Artista" , SqlDbType.NVarChar) { Value = evento.Artista },
                new SqlParameter("@In_Descripcion" , SqlDbType.NVarChar) {Value = evento.Descripcion},
                new SqlParameter("@In_Ubicacion" , SqlDbType.NVarChar) {Value = evento.Ubicacion},
                new SqlParameter("@In_Imagen" , SqlDbType.VarBinary) {Value = evento.Imagen},
                new SqlParameter("@In_Horario" , SqlDbType.Time) {Value = evento.Horario},
                new SqlParameter("@In_Fecha" , SqlDbType.Date) {Value = evento.Fecha},
                new SqlParameter("@In_Id_Planificador" , SqlDbType.Int) {Value = asociado_actual.Id},
                new SqlParameter("@In_PublicoObjetivo" , SqlDbType.NVarChar) {Value = evento.PublicoObjetivo } 
            };

            return Convert.ToInt32(conn.WriteWithReturn("SP_InsertEvento", parameters));
        }

        public double GetMontoTotal(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter ("@In_IdEvento" , SqlDbType.Int){ Value = id },
            };

            return Convert.ToInt32(conn.WriteWithReturn("SP_GetMontoEvento  ", parameters));
        }

        public DataTable getInforme()
        {
            return conn.Read("SP_GetInformeEventos", null);
        }

        public List<string> GetGustosMusicales()
        {
            DataTable table = conn.Read("SP_GetPublicosPosibles", null);
            List<string> list = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(row["Gusto"].ToString()); ;
            }
            return list;
        }

        public void InsertGustoCliente(int id, object publico)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter ("@In_IdCliente" , SqlDbType.Int){ Value = id },
                new SqlParameter("@In_Gusto" , SqlDbType.NVarChar) {Value = publico},
            };
            conn.Write("SP_InsertGustoCliente ", parameters);
        }

        public List<int> GetClientesXGusto(string publicoObjetivo)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Gusto" , SqlDbType.NVarChar) {Value = publicoObjetivo},
            };
            DataTable table = conn.Read("SP_GetGustosClientes", parameters);
            List<int> list = new List<int>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(int.Parse(row["Id_Cliente"].ToString()));
            }
            return list;
        }
    }
}
