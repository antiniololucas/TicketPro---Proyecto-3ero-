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
                    foreach (var item in GetPermisosFamilia(per) )
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
    }
}
