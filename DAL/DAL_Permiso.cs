using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Permiso
    {

        DBConnection conn;
        public DAL_Permiso()
        {
            conn = DBConnection.GetInstance();
        }

        public List<IPermiso> GetPorUser(EntityUser user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@In_Id_Rol", SqlDbType.Int) { Value = user.Rol.Id },
            };
            DataTable table = conn.Read("SP_GetPermisos_User", parameters);
            List<IPermiso> permisos = new List<IPermiso>();
            foreach (DataRow row in table.Rows)
            {
                if (!bool.Parse(row["Is_Familia"].ToString()))
                {
                    EntityPermiso per = SqlMapper.MapPermiso(row);
                    permisos.Add(per);
                }
                else
                {
                    EntityFamilia per = SqlMapper.MapFamilia(row);
                    per.Permisos = GetPermisosFamilia(per);
                    permisos.Add(per);
                }
            }
            return permisos;
        }

        public List<IPermiso> GetPermisosFamilia(EntityFamilia familia)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@In_Id_familia", SqlDbType.Int) { Value = familia.Id },
            };
            DataTable table = conn.Read("SP_GetPermisosFamilia", parameters);
            List<IPermiso> permisos = new List<IPermiso>();
            foreach (DataRow row in table.Rows)
            {

                if (bool.Parse(row["Is_Familia"].ToString()))
                {
                    EntityFamilia per = SqlMapper.MapFamilia(row);
                    foreach (var item in GetPermisosFamilia(per))
                    {
                        permisos.Add(item);
                    }
                }
                else
                {
                    permisos.Add(SqlMapper.MapPermiso(row));
                }
            }
            return permisos;
        }

        public List<IPermiso> GetAll()
        {
            DataTable table = conn.Read("SP_GetAllPermisos", null);
            List<IPermiso> permisos = new List<IPermiso>();
            foreach (DataRow row in table.Rows)
            {

                if (bool.Parse(row["Is_Familia"].ToString()))
                {
                    EntityFamilia per = SqlMapper.MapFamilia(row);
                    foreach (var item in GetPermisosFamilia(per))
                    {
                        per.Permisos.Add(item);
                    }
                    permisos.Add(per);
                }
                else
                {
                    permisos.Add(SqlMapper.MapPermiso(row));
                }
            }
            return permisos;
        }

        public bool deleteFamilia(IPermiso permisoActual)
        {
            if (permisoActual is EntityFamilia)
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@In_IdFamilia", SqlDbType.Int) { Value =  permisoActual.Id}
                };
                return conn.Write("SP_DeleteFamilia", parameters);
            }
            else { return false; }

        }

        public bool InsertFamilia(List<IPermiso> permisosFamiliaElegida, string nombre)
        {
            bool ok = true;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_Nombre", SqlDbType.NVarChar) { Value =  nombre}
            };
            var result = conn.WriteWithReturn("SP_InsertPermiso", parameters);
            foreach (var item in permisosFamiliaElegida)
            {
                SqlParameter[] parameters2 = new SqlParameter[]
                {
                new SqlParameter("@In_IdPermiso", SqlDbType.Int) { Value =  item.Id},
                new SqlParameter("@In_IdFamilia", SqlDbType.Int) { Value =  Convert.ToInt32(result)},
                new SqlParameter("@In_Nombre", SqlDbType.NVarChar) { Value =  nombre}
                };
                bool resultadoWrite = conn.Write("SP_InsertFamilia", parameters2);
                if (resultadoWrite is false) ok = false;
            }
            return ok;
        }

        public bool UpdateFamilia(EntityFamilia familiaActual, bool changeName)
        {
            bool ok = true;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@In_IdPermiso", SqlDbType.Int) { Value =  familiaActual.Id},
                new SqlParameter("@In_Nombre", SqlDbType.NVarChar) { Value =  familiaActual.Nombre}
            };
            ok = conn.Write("SP_UpdateFamilia", parameters);
            //Agrego uno por uno en FamiliaPermiso
            foreach (var item in familiaActual.Permisos)
            {
                SqlParameter[] parameters2 = new SqlParameter[]
            {
                new SqlParameter("@In_IdPermiso", SqlDbType.Int) { Value =  item.Id},
                new SqlParameter("@In_IdFamilia", SqlDbType.Int) { Value =  familiaActual.Id},
                new SqlParameter("@In_Nombre", SqlDbType.NVarChar) { Value =  familiaActual.Nombre}
            };
                bool result = conn.Write("SP_InsertFamilia", parameters2);
                if(!result) ok = false;
            }
            return ok;
        }
    }
}
