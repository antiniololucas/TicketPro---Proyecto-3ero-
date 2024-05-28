using BE;
using BLL;
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
    public partial class FormGestionUsuario : ServiceForm
    {
        BusinessUser _businessUser = new BusinessUser();
        List<EntityUser> users_error = new List<EntityUser>();
        EntityUser userActual;

        public FormGestionUsuario()
        {
            InitializeComponent();
            llenarDG();
        }

        private void llenarDG()
        {
            users_error = _businessUser.GetUsers();
            DGusers.DataSource = null;
            DGusers.DataSource = users_error;
            lblCantUser.Text = users_error.Count.ToString();
            DGusers.Columns["Id"].Visible = false;
            DGusers.Columns["Password"].Visible = false;
            DGusers.Columns["Username"].Visible = false;
            DGusers.Columns["IsBlock"].HeaderText = "Bloqueado";
        }

        private void DGusers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1) { return; }
            TxtNombre.Text = DGusers.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            TxtApellido.Text = DGusers.Rows[e.RowIndex].Cells["Apellido"].Value.ToString();
            TxtDni.Text = DGusers.Rows[e.RowIndex].Cells["Dni"].Value.ToString();
            TxtRol.Text = DGusers.Rows[e.RowIndex].Cells["Rol"].Value.ToString();
            userActual = DGusers.SelectedRows[0].DataBoundItem as EntityUser;
            activarButtons();
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que quiere concretar la operación?", "Confirmación", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }

            var response = _businessUser.ChangeBlockUser(userActual);
            RevisarRespuestaServicio(response);
            if (response.Ok) { llenarDG(); limpiarTxt(); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro?", "Confirmación", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }

            var response = _businessUser.EliminarUsuario(userActual.Id);
            RevisarRespuestaServicio(response);
            if (response.Ok) { llenarDG(); limpiarTxt(); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.No) { return; }

            if (string.IsNullOrEmpty(TxtApellido.Text) || string.IsNullOrEmpty(TxtNombre.Text) || string.IsNullOrEmpty(TxtRol.Text))
            {
                MessageBox.Show("Camnpos incompletos"); return;
            }

            if (!int.TryParse(TxtDni.Text, out int val)) { MessageBox.Show("El dni debe ser numerico"); return; }

            EntityUser user = new EntityUser()
            {
                Dni = val,
                Apellido = TxtApellido.Text,
                Nombre = TxtNombre.Text,
                Rol = TxtRol.Text,
            };
            var response = _businessUser.AgregarUsuario(user);
            RevisarRespuestaServicio(response);
            if (response.Ok) { llenarDG(); limpiarTxt(); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (!int.TryParse(TxtDni.Text, out int val)) { MessageBox.Show("El dni debe ser numerico"); return; }
                EntityUser user = new EntityUser()
                {
                    Id = userActual.Id,
                    Dni = val,
                    Apellido = TxtApellido.Text,
                    Nombre = TxtNombre.Text,
                    Rol = TxtRol.Text,
                };
                var response = _businessUser.ModificarUsuario(user);
                RevisarRespuestaServicio(response);
                if (response.Ok) { llenarDG(); limpiarTxt(); }
            }
        }

        private void activarButtons()
        {
            btnEliminar.Enabled = true;
            btnDesbloquear.Enabled = true;
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
            TxtRol.Text = string.Empty;
            desactivarButons();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarTxt();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FormInicio frm = new FormInicio();
            frm.Show();
            this.Close();
        }
    }
}




