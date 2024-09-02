using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormBackupRestore : ServiceForm

    { 
        public FormBackupRestore()
        {
            InitializeComponent();
            ChangeTranslation();
        }
        private BusinessBackupRestore _bllBackupRestore = new BusinessBackupRestore();


        private void btnRealizarBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBackupPath.Text))
            {
                RevisarRespuestaServicio(_bllBackupRestore.RealizarBackup(txtBackupPath.Text));
                txtBackupPath.Text = "";
            }
            else
            {
                MessageBox.Show(SearchTraduccion( "PathIncompleto"));
            }
        }

        private void btnRealizarRest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRestorePath.Text))
            {
               RevisarRespuestaServicio(_bllBackupRestore.RealizarRestore(txtRestorePath.Text));
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
