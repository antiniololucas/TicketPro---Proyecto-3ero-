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
using System.Text.RegularExpressions;
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FormInicio frm = new FormInicio();
            frm.Show();
            this.Close();
        }

        private void btnCambiarClave_Click_1(object sender, EventArgs e)
        {
            bool huboError = false;
            EsconderLabelError(new List<Bunifu.UI.WinForms.BunifuLabel> { lblErrorActual, lblErrorNueva, lblErrorRepetida });
            //Campos vacíos

            if (string.IsNullOrEmpty(TxtPasswordVieja.Text)) { MostrarLabelError("Campos incompleto", lblErrorActual); huboError = true; }
            if (string.IsNullOrEmpty(TxtPasswordNueva.Text)) { MostrarLabelError("Campo Incompleto", lblErrorNueva); huboError = true; }
            if (string.IsNullOrEmpty(TxtPasswordRepetida.Text)) { MostrarLabelError("Campo Incompleto", lblErrorRepetida); huboError = true; }

            if(huboError) { return; }

            //Contraseñas nuevas no coinciden
            if (TxtPasswordNueva.Text != TxtPasswordRepetida.Text)
            {
                MostrarLabelError("No coinciden", lblErrorNueva);
                MostrarLabelError("No coinciden", lblErrorRepetida);
                return;
            }

            //Validar regex de la nueva contraseña
            if (!RegexValidation.IsValidPassword(TxtPasswordNueva.Text))
            {
                MostrarLabelError("No cumplen con los requisitos", lblErrorNueva);
                MostrarLabelError("No cumplen con los requisitos", lblErrorRepetida);
                return;
            }

            //Contraseña original coincide con la del user (Ultimo para brindar está información la menor cantidad de veces)
            if (CryptoManager.EncryptString(TxtPasswordVieja.Text) != user.Password)
            {
                MostrarLabelError("Incorrecta", lblErrorActual); return;
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
    }
}
