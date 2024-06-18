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
            CustomizeDesing();
        }

        private void CustomizeDesing()
        {
            PanelSubMenuAdmin.Visible = false;
            PanelSubMenuUsuario.Visible = false;
        }

        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            PanelSubMenuAdmin.Visible = !PanelSubMenuAdmin.Visible;

        }

        private void BtnUsuario_Click(object sender, EventArgs e)
        {
            PanelSubMenuUsuario.Visible = !PanelSubMenuUsuario.Visible;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormInicioSesion frm = new FormInicioSesion();
            frm.Show();
            this.Close();
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            FormCambiarClave frm = new FormCambiarClave();
            frm.Show();
            this.Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar la sesión?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.No) { return; }
            SessionManager.Logout();
            FormInicioSesion frm = new FormInicioSesion(); 
            this.Close();
            frm.Show();    
        }

        private void btnGestionUsuario_Click(object sender, EventArgs e)
        {
            FormGestionUsuario frm = new FormGestionUsuario();
            frm.Show();
            this.Hide();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            FormInicioSesion frm = new FormInicioSesion();
            frm.Show();
            this.Close();
        }

        private void btnVentaEntradas_Click(object sender, EventArgs e)
        {
            PanelSubMenuEntradas.Visible = !PanelSubMenuEntradas.Visible;
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            FormGenerarFactura frm = new FormGenerarFactura();
            frm.Show();
            this.Close();
        }

        private void btnCobrarFactura_Click(object sender, EventArgs e)
        {
            FormCobrarFactura frm = new FormCobrarFactura();
            frm.Show();
            this.Close();
        }

        private void btnMaestros_Click(object sender, EventArgs e)
        {

        }
    }
}
