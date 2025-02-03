namespace DAL
{
    public class DAL_BackupRestore
    {
        DBConnection conn;
        public DAL_BackupRestore()
        {
            conn = DBConnection.GetInstance();
        }
        public bool RealizarBackup(string backupPath)
        {
            return conn.RealizarBackup(backupPath);
        }
        public bool RealizarRestore(string backupFilePath)
        {
            return conn.RealizarRestore(backupFilePath);
        }


    }
}
