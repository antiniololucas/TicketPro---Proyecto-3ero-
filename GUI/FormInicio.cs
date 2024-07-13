using BLL;
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
        private readonly SessionManager _sessionManager = SessionManager.GetInstance();
        private BusinessPermiso _businessPermiso;

        public FormInicio()
        {
            InitializeComponent();
            _businessPermiso = new BusinessPermiso();
            validateRol();
        }

        private void validateRol()
        {
            _sessionManager.User.Rol.Permisos = _businessPermiso.getPermisosPorUser(_sessionManager.User).Data;
            
            PanelBtnAdmin.Visible = _sessionManager.HasPermission(7);
            PanelBtnVenta.Visible = _sessionManager.HasPermission(1);
            PanelBtnCobranza.Visible = _sessionManager.HasPermission(2);
            PanelBtnUsuario.Visible = _sessionManager.HasPermission(3);
            PanelBtnMaestros.Visible = _sessionManager.HasPermission(4);
            //Falta btn reporte
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

        private void btnCobranza_Click(object sender, EventArgs e)
        {
            panelSubMenuCobranza.Visible = !panelSubMenuCobranza.Visible;
        }

        private void btnMaestros_Click(object sender, EventArgs e)
        {
            panelSubMenuMaestros.Visible = !panelSubMenuMaestros.Visible;
        }

        private void btnCobranza_Click_1(object sender, EventArgs e)
        {
            panelSubMenuCobranza.Visible = !panelSubMenuCobranza.Visible;
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

        private void btnMaestroClientes_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormMaestroClientes());
        }

        private void btnCobrarFactura_Click(object sender, EventArgs e)
        {
            CambiarForm( new FormCobrarFactura());
        }

        private void btnMaestrosCliente_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormMaestroClientes());
        }

        private void BtnAyuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comuniquese con el administrador por medio del siguiente mail:\nLucasPablo.Antinolo@alumnos.uai.edu.ar", 
                "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGestionPefiles_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormGestionRol());
        }

        private void btnGestionFamilias_Click(object sender, EventArgs e)
        {
            CambiarForm( new FormGestionFamilias());
        }
    }
}
