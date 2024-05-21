using BE;
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
    public partial class FormCambiarClave : ServiceForm
    {
        public FormCambiarClave()
        {
            InitializeComponent();
        }

        EntityUser user = SessionManager.GetInstance().User;
        BusinessUser _businessUser = new BusinessUser();

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TxtPasswordNueva.Text) || string.IsNullOrEmpty(TxtPasswordVieja.Text) || string.IsNullOrEmpty(TxtPasswordRepetida.Text))
            {
                MessageBox.Show("Campos incompletos", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            if (TxtPasswordNueva.Text != TxtPasswordRepetida.Text)
            {
                MessageBox.Show("Las contraseñas nuevas no coinciden", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            if (CryptoManager.EncryptString(TxtPasswordVieja.Text) != user.Password)
            {
                bool ok = false;
                string mensaje = "La contraseña actual es incorrecta";
                RevisarRespuestaServicio(new BLL.BusinessResponse<bool>(ok, false, mensaje));
                return;
            }

            var response = _businessUser.CambiarClave(user.Id, TxtPasswordNueva.Text);
            RevisarRespuestaServicio(response);
            if (response.Ok) 
            { 
                SessionManager.Logout();
                FormInicioSesion frm = new FormInicioSesion();
                frm.Show();
                this.Close();
            }
        }

        private void btnVolverInicio_Click(object sender, EventArgs e)
        {
            FormInicio frm = new FormInicio();
            frm.Show();
            this.Close();
        }
    }
}
