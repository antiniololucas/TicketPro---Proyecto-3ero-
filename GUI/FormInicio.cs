using Services;
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
    public partial class FormInicio : ServiceForm
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            PanelSubMenuAdmin.Visible = !PanelSubMenuAdmin.Visible;
        }

        private void BtnUsuario_Click(object sender, EventArgs e)
        {
            PanelSubMenuUsuario.Visible = !PanelSubMenuUsuario.Visible;
        }

        private void btnVentaEntradas_Click(object sender, EventArgs e)
        {
            PanelSubMenuEntradas.Visible = !PanelSubMenuEntradas.Visible;
        }

        private void btnMaestros_Click(object sender, EventArgs e)
        {
            panelSubMenuMaestros.Visible = !panelSubMenuMaestros.Visible;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar la sesión?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.No) { return; }
            SessionManager.Logout();
            CambiarForm(new FormInicioSesion());  
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormCambiarClave());
        }

        private void btnGestionUsuario_Click(object sender, EventArgs e)
        {
            CambiarForm( new FormGestionUsuario());
        }

        private void BtnRegistrarCliente_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormRegistrarCliente());
        }

        private void btnGenerarFactura_Click_1(object sender, EventArgs e)
        {
            CambiarForm(new FormGenerarFactura());
        }

        private void btnCobrarFactura_Click_1(object sender, EventArgs e)
        {
            CambiarForm(new FormCobrarFactura());
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaestroClientes_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormMaestroClientes());
        }

    }
}
