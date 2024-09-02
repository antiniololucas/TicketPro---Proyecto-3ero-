using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class DBConnection
    {
        private static DBConnection _instance;

        private readonly SqlConnection _connection;
        private DBConnection()
        {
            _connection = new SqlConnection("Data Source=.; Initial Catalog=TicketPro; Integrated Security=True");
        }

        public static DBConnection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DBConnection();
            }

            return _instance;
        }

        private void OpenConnection()
        {
            _connection.Open();
        }

        private void CloseConnection()
        {
            _connection.Close();
        }

        public DataTable Read(string sp, SqlParameter[] parameters)
        {
            OpenConnection();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = sp
                }
            };

            if (parameters != null)
            {
                sqlDataAdapter.SelectCommand.Parameters.Clear();
                sqlDataAdapter.SelectCommand.Parameters.AddRange(parameters);
            }

            sqlDataAdapter.SelectCommand.Connection = _connection;

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            CloseConnection();

            return dataTable;
        }

        public bool Write(string sp, SqlParameter[] parameters)
        {
            if (parameters.Length == 0) return false;

            int canInsert = -1;

            OpenConnection();
            SqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = sp,
                    Connection = _connection,
                    Transaction = transaction
                };

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(parameters);

                canInsert = sqlCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }

            CloseConnection();

            return canInsert != -1;
        }

        public object WriteWithReturn(string storedProcedure, SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();

                using (SqlCommand command = new SqlCommand(storedProcedure, _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    return command.ExecuteScalar();
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool RealizarBackup(string backupPath)
        {
            string nombreArchivo = $"TicketPro.BCK_{DateTime.Now:ddMMyy_HHmm}.bak";
            string rutaCompleta = System.IO.Path.Combine(backupPath, nombreArchivo);
            string comandoBackup = $"BACKUP DATABASE TicketPro TO DISK='{rutaCompleta}'";
            SqlCommand cmd = new SqlCommand(comandoBackup, _connection);
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
            return true;
        }
        public bool RealizarRestore(string backupFilePath)
        {
            try
            {
                _connection.Open();

                using (SqlCommand setMaster = new SqlCommand("USE master;", _connection))
                {
                    setMaster.ExecuteNonQuery();
                }

                using (SqlCommand setSingleUser = new SqlCommand("ALTER DATABASE TicketPro SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", _connection))
                {
                    setSingleUser.ExecuteNonQuery();
                }

                string query = $"RESTORE DATABASE TicketPro FROM DISK = '{backupFilePath}' WITH REPLACE;";
                using (SqlCommand cmd = new SqlCommand(query, _connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand setMultiUser = new SqlCommand("ALTER DATABASE TicketPro SET MULTI_USER;", _connection))
                {
                    setMultiUser.ExecuteNonQuery();
                }
                _connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
