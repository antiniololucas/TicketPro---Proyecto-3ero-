using DAL;

namespace BLL
{
    public class BusinessBackupRestore
    {
        private DAL_BackupRestore dalbackup = new DAL_BackupRestore();

        public BusinessResponse<bool> RealizarBackup(string backupPath)
        {
            bool ok = dalbackup.RealizarBackup(backupPath);
            string mensaje = ok is true ? "BackUpCompletado" : "Error";
            return new BusinessResponse<bool>(ok, false, mensaje);
        }
        public BusinessResponse<bool> RealizarRestore(string restorePath)
        {
            bool ok = dalbackup.RealizarRestore(restorePath);
            string mensaje = ok is true ? "RestoreCompletado" : "Error";
            return new BusinessResponse<bool>(ok, false, mensaje);
        }
    }
}
