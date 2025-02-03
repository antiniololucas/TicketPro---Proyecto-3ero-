using BLL;
using Services;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormError : ServiceForm
    {
        private BusinessBackupRestore _bllBackupRestore;

        public FormError()
        {
            InitializeComponent();
            ChangeTranslation();
            LoadData();

        }

        private void LoadData()
        {
            if (_sessionManager.HasPermission(7))
            {
                panelAdmin.Visible = true;
                return;
            }
            PanelErrorNormal.Visible = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnSalirError2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnRealizarRest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRestorePath.Text))
            {
                MessageBox.Show(SearchTraduccion("PathIncompleto"));
                return;
            }

            var response = _bllBackupRestore.RealizarRestore(txtRestorePath.Text);
            RevisarRespuestaServicio(response);
            if (response.Ok)
            {
                guardarEventoBitacora("Restore Realizado", 3);
                txtRestorePath.Text = "";
                MessageBox.Show(SearchTraduccion("IngreseNuevamente"));
                SessionManager.Logout();
                CambiarForm(new FormInicioSesion());
            }
            MessageBox.Show(SearchTraduccion("Error"));
        }

        private void btnRecalcularDV_Click(object sender, EventArgs e)
        {
            UpdateDigitoVerificador();
            MessageBox.Show(SearchTraduccion("IngreseNuevamente"));
            SessionManager.Logout();
            CambiarForm(new FormInicioSesion());
        }
    }
}
