using BE;
using BLL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormInicioSesion : ServiceForm
    {
        public FormInicioSesion()
        {
            InitializeComponent();
            AcceptButton = btnIngresar;
            TxtUser.Select();
            FillIdiomas();
            this.nombre_modulo = "Inicio";
        }

        BusinessUser _businessUser = new BusinessUser();
        SessionManager _sessionManager;
        List<EntityUser> ListaErrores = new List<EntityUser>();
        private BusinessPermiso _businessPermiso = new BusinessPermiso();

        private void FillIdiomas()
        {
            cmbCambiarIdioma.Items.Clear();
            BusinessResponse<List<EntityIdioma>> response = _businessIdioma.GetAll();
            cmbCambiarIdioma.Items.AddRange(response.Data.ToArray());
            cmbCambiarIdioma.DisplayMember = "Idioma";
            cmbCambiarIdioma.SelectedIndex = 0;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            bool HuboError = false;

            if (string.IsNullOrEmpty(TxtUser.Text))
            {
                MessageBox.Show(SearchTraduccion("Campos_incompletos", cmbCambiarIdioma.SelectedItem as EntityIdioma));
                HuboError = true;
            }

            if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                MessageBox.Show(SearchTraduccion("Campos_incompletos", cmbCambiarIdioma.SelectedItem as EntityIdioma));
                HuboError = true;
            }

            if (HuboError) return;

            BusinessResponse<EntityUser> response = _businessUser.VerificarCredenciales(TxtUser.Text, TxtPassword.Text);

            RevisarRespuestaServicio(response, cmbCambiarIdioma.SelectedItem as EntityIdioma);

            if (!response.Ok && response.Data?.IsBlock == false)
            {
                ListaErrores.Add(response.Data);

                if (ListaErrores.Where(users => users.Id == response.Data.Id).Count() == 3)
                {
                    RevisarRespuestaServicio(_businessUser.ChangeBlockUser(response.Data), cmbCambiarIdioma.SelectedItem as EntityIdioma);
                }
            }

            //Ingresa bien las credenciales.
            if (response.Ok)
            {
                try
                {
                    _sessionManager = SessionManager.Login(response.Data, cmbCambiarIdioma.SelectedItem as EntityIdioma);
                    _sessionManager.User.Rol.Permisos = _businessPermiso.getPermisosPorUser(_sessionManager.User).Data;
                    if (ValidarDigitoVerificador())
                    {
                        guardarEventoBitacora("Inicio de Sesión", 1);
                        FormInicio frm = new FormInicio();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        FormError frm = new FormError();
                        frm.Show();
                    }
                }
                catch (Exception ex)
                {
                    RevisarRespuestaServicio(new BusinessResponse<bool>(false, false, ex.Message), cmbCambiarIdioma.SelectedItem as EntityIdioma);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbCambiarIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityIdioma idioma = cmbCambiarIdioma.SelectedItem as EntityIdioma;

            IPublisher publisher = new LanguageManager();
            publisher.AddObserver(this);
            publisher.NotifyAll(idioma);
        }
    }
}
