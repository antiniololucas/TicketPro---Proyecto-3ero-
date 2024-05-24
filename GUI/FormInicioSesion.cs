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
            AcceptButton = btnIngresar;
        }

        BusinessUser _businessUser = new BusinessUser();
        SessionManager _sessionManager;
        List<EntityUser> ListaErrores = new List<EntityUser>();

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

            BusinessResponse<EntityUser> response = _businessUser.VerificarCredenciales(TxtUser.Text, TxtPassword.Text);

            RevisarRespuestaServicio(response);

            if (!response.Ok && response.Data?.IsBlock == false)
            {
                ListaErrores.Add(response.Data);

                if (ListaErrores.Where(users => users.Id == response.Data.Id).Count() == 3)
                {
                    RevisarRespuestaServicio(_businessUser.BlockUser(response.Data.Id));
                }
            }

            //Ingresa bien las credenciales.
            if (response.Ok)
            {
                try
                {
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
            Application.Exit();
        }
    }
}
