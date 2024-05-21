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
        List<EntityUser> users = new List<EntityUser>();
        int userActual;
        public FormGestionUsuario()
        {
            InitializeComponent();
            llenarDG();
        }

        private void llenarDG()
        {
            users = _businessUser.GetUsers();
            DGusers.DataSource = null;
            DGusers.DataSource = users;
            lblCantUser.Text = users.Count.ToString();
            DGusers.Columns["Id"].Visible = false;
            DGusers.Columns["Password"].Visible = false;
        }

        private void DGusers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1) { return; }
            TxtNombre.Text = DGusers.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            TxtApellido.Text = DGusers.Rows[e.RowIndex].Cells["Apellido"].Value.ToString();
            TxtDni.Text = DGusers.Rows[e.RowIndex].Cells["Dni"].Value.ToString();
            TxtRol.Text = DGusers.Rows[e.RowIndex].Cells["Rol"].Value.ToString();
            userActual = Convert.ToInt32(DGusers.Rows[e.RowIndex].Cells["Id"].Value);
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que quiere concretar la operación?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                var response = _businessUser.UnblockUser(userActual);
                RevisarRespuestaServicio(response);
                if (response.Ok) { llenarDG(); }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que quiere concretar la operación?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                var response = _businessUser.EliminarUsuario(userActual);
                RevisarRespuestaServicio(response);
                if (response.Ok) { llenarDG(); }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que quiere concretar la operación?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
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
                if (response.Ok) { llenarDG(); }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que quiere concretar la operación?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes) { }
            if (!int.TryParse(TxtDni.Text, out int val)) { MessageBox.Show("El dni debe ser numerico"); return; }
            EntityUser user = new EntityUser()
            {
                Id = userActual,
                Dni = val,
                Apellido = TxtApellido.Text,
                Nombre = TxtNombre.Text,
                Rol = TxtRol.Text,
            };
            var response = _businessUser.ModificarUsuario(user);
            RevisarRespuestaServicio(response);
            if (response.Ok) { llenarDG(); }
        }
    }

    /* DialogResult result = MessageBox.Show("¿QEstá seguro que quiere concretar la operación?", "Confirmación", MessageBoxButtons.YesNo);

     if (result == DialogResult.Yes)
     {
         Console.WriteLine("Continuar...");
         // Aquí puedes agregar el código para continuar con la lógica de tu aplicación.
     }
     else
     {
         Console.WriteLine("No continuar...");
         // Aquí puedes agregar el código para manejar la negación de continuar.
     }*/
}




