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


    }
}