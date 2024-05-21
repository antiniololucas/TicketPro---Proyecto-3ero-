using BE;
using BLL;
using Bunifu.UI.WinForms;
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
    public partial class FormInicioSesion : ServiceForm
    {
        public FormInicioSesion()
        {
            InitializeComponent();
        }

        BusinessAuth _businessAuth = new BusinessAuth();
        BusinessUser _businessUser = new BusinessUser();
        SessionManager _sessionManager;
        int contadorBloqueo = 0;

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            EsconderLabelError(new List<BunifuLabel>()
            {
                lblErrorUser,
                lblErrorPassword
            });

            bool HuboError = false;

            if (string.IsNullOrEmpty(TxtUser.Text))
            {
                MostrarLabelError("Debe ingresar usuario", lblErrorUser);
                HuboError = true;
            }

            if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                MostrarLabelError("Debe ingresar contraseña", lblErrorPassword);
                HuboError = true;
            }

            if (HuboError) return;

            BusinessResponse<EntityUser> response = _businessAuth.VerificarCredenciales(TxtUser.Text, TxtPassword.Text);

            RevisarRespuestaServicio(response);

            //Para el bloqueo a los 3 intentos:
            if(!response.Ok && response.Data?.Username != null)
            {
                contadorBloqueo++;
                if(contadorBloqueo == 3)
                {
                    RevisarRespuestaServicio(_businessUser.BlockUser(response.Data.Id));
                }
            }

            //Ingresa bien las credenciales.
            if (response.Ok)
            {
                try
                {
                    contadorBloqueo = 0;
                    _sessionManager = SessionManager.Login(response.Data);
                    FormInicio frm = new FormInicio();
                    frm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    RevisarRespuestaServicio(new BusinessResponse<bool>(false, false, ex.Message));
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
