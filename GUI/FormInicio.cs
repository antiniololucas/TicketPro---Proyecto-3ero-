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
            this.Hide();
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            FormCambiarClave frm = new FormCambiarClave();
            frm.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
            this.Close();
        }

        private void btnGestionUsuario_Click(object sender, EventArgs e)
        {
            FormGestionUsuario frm = new FormGestionUsuario();
            frm.Show();
            this.Hide();
        }
    }
}
