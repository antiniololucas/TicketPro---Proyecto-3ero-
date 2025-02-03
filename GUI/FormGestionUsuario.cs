using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormGestionUsuario : ServiceForm
    {
        BusinessUser _businessUser = new BusinessUser();
        List<EntityRol> roles;
        BusinessRol businessRol = new BusinessRol();
        List<EntityUser> users_error = new List<EntityUser>();
        EntityUser userActual;

        public FormGestionUsuario()
        {
            InitializeComponent();
            roles = businessRol.BuscarRoles().Data;
            LLenarCmb(cmbRoles, roles, "Nombre");
            ActualizarData();
            ChangeTranslation();
            this.nombre_modulo = "Admin";
        }

        private void ActualizarData()
        {
            users_error = _businessUser.GetUsers();
            lblCantUser.Text = users_error.Count.ToString();
            LlenarDG(DGusers, users_error, new List<string>() { "Id", "Password", "Username" });
            DGusers.Columns["IsBlock"].HeaderText = "Bloqueado";
        }

        private void DGusers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1) { return; }
            TxtNombre.Text = DGusers.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            TxtApellido.Text = DGusers.Rows[e.RowIndex].Cells["Apellido"].Value.ToString();
            TxtDni.Text = DGusers.Rows[e.RowIndex].Cells["Dni"].Value.ToString();
            userActual = DGusers.SelectedRows[0].DataBoundItem as EntityUser;
            cmbRoles.SelectedIndex = roles.IndexOf(roles.FirstOrDefault(Rol => Rol.Id == userActual.Rol.Id));
            if (userActual.IsBlock == true) { btnDesbloquear.Enabled = false; }
            activarButtons();
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }

            var response = _businessUser.ChangeBlockUser(userActual);
            RevisarRespuestaServicio(response);
            if (response.Ok) { guardarEventoBitacora("Bloqueo de un usuario", 4); ActualizarData(); limpiarTxt(); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }

            var response = _businessUser.EliminarUsuario(userActual.Id);
            RevisarRespuestaServicio(response);
            if (response.Ok) { guardarEventoBitacora("Eliminación de un usuario", 5); ActualizarData(); limpiarTxt(); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "", MessageBoxButtons.YesNo);

            if (result == DialogResult.No) { return; }

            if (string.IsNullOrEmpty(TxtApellido.Text) || string.IsNullOrEmpty(TxtNombre.Text))
            {
                MessageBox.Show("Camnpos incompletos"); return;
            }

            if (!int.TryParse(TxtDni.Text, out int val)) { MessageBox.Show(SearchTraduccion("FormatoIncorrecto")); return; }

            EntityUser user = new EntityUser()
            {
                Dni = val,
                Apellido = TxtApellido.Text,
                Nombre = TxtNombre.Text,
                Rol = cmbRoles.SelectedItem as EntityRol
            };
            var response = _businessUser.AgregarUsuario(user);
            RevisarRespuestaServicio(response);

            if (response.Ok) { guardarEventoBitacora("Se creó un usuario", 4); ActualizarData(); limpiarTxt(); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (!int.TryParse(TxtDni.Text, out int val)) { MessageBox.Show(SearchTraduccion("FormatoIncorrecto")); return; }
                EntityUser user = new EntityUser()
                {
                    Id = userActual.Id,
                    Dni = val,
                    Apellido = TxtApellido.Text,
                    Nombre = TxtNombre.Text,
                    Rol = cmbRoles.SelectedItem as EntityRol
                };
                var response = _businessUser.ModificarUsuario(user);
                RevisarRespuestaServicio(response);
                if (response.Ok) { guardarEventoBitacora("Modificación de un usuario", 5); ActualizarData(); limpiarTxt(); }
            }
        }

        private void activarButtons()
        {
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;
        }

        private void desactivarButons()
        {
            btnEliminar.Enabled = false;
            btnDesbloquear.Enabled = false;
            btnModificar.Enabled = false;
            btnAgregar.Enabled = true;
        }

        private void limpiarTxt()
        {
            TxtNombre.Text = string.Empty;
            TxtApellido.Text = string.Empty;
            TxtDni.Text = string.Empty;
            cmbRoles.SelectedIndex = -1;
            desactivarButons();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarTxt();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

    }
}




