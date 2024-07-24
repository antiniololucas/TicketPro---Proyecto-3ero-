using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace DAL
{
    public class DAL_Rol
    {

        DBConnection conn;

        public DAL_Rol()
        {
            conn = DBConnection.GetInstance();
        }

        public bool InsertRol (string nombre, List<IPermiso> permisos)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@In_Nombre" , SqlDbType.NVarChar) { Value = nombre },
           };

            var result = conn.WriteWithReturn("SP_InsertRol", parameters);
            int newId = Convert.ToInt32(result);
            insertRolPermiso(permisos, newId);
            if (result is null) { return false; }

            return true;
        }

        public void insertRolPermiso(List<IPermiso> permisos, int newId) 
        {
            foreach (var permiso in permisos)
            {
                SqlParameter[] parameters = new SqlParameter[]
                {   
                    new SqlParameter("@In_IdPermiso" , SqlDbType.Int) { Value = permiso.Id },
                    new SqlParameter("@In_IdRol" , SqlDbType.Int) { Value = newId },
                };
                 conn.Write("SP_InsertRolPermiso", parameters);
            }
        }

        public List<EntityRol> GetAll()
        {
            DataTable table = conn.Read("SP_GetRoles", null);
            List<EntityRol> roles = new List<EntityRol>();
            foreach (DataRow row in table.Rows)
            {
                EntityRol rol = SqlMapper.MapRol(row);
                roles.Add(rol);
            }
            return roles;
        }

        public bool UpdateRol(EntityRol rol)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@In_IdRol" , SqlDbType.Int) { Value = rol.Id },
                    new SqlParameter("@In_Nombre" , SqlDbType.NVarChar) { Value = rol.Nombre },
            };
            var result = conn.WriteWithReturn("SP_UpdateRol", parameters);
            int resultado = Convert.ToInt32(result);
            insertRolPermiso(rol.Permisos, resultado);
            return resultado != 0;
        }

        public bool deleteRol(EntityRol rol)
        {
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter("@In_IdRol" , SqlDbType.Int){ Value = rol.Id }
            };
            return conn.Write("SP_DeleteRol", parameters);
        }
    }
}