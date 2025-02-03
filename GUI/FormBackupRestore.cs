using BLL;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormBackupRestore : ServiceForm

    {
        public FormBackupRestore()
        {
            InitializeComponent();
            ChangeTranslation();
            this.nombre_modulo = "BackupRestore";
        }
        private BusinessBackupRestore _bllBackupRestore = new BusinessBackupRestore();


        private void btnRealizarBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBackupPath.Text))
            {
                var response = _bllBackupRestore.RealizarBackup(txtBackupPath.Text);
                RevisarRespuestaServicio(response);
                if (response.Ok) guardarEventoBitacora("Backup Realizado", 3);
                txtBackupPath.Text = "";
            }
            else
            {
                MessageBox.Show(SearchTraduccion("PathIncompleto"));
            }
        }

        private void btnRealizarRest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRestorePath.Text))
            {
                var response = _bllBackupRestore.RealizarRestore(txtRestorePath.Text);
                RevisarRespuestaServicio(response);
                if (response.Ok) guardarEventoBitacora("Restore Realizado", 3);
                txtRestorePath.Text = "";

            }
            else
            {
                MessageBox.Show(SearchTraduccion("PathIncompleto"));
            }
        }

        private void btnSeleccionarBackUp_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtBackupPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnSeleccionarRestore_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "SQL Backup Files (*.bak)|*.bak";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtRestorePath.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }
    }
}
