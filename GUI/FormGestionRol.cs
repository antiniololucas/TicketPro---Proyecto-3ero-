using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormGestionRol : ServiceForm
    {

        BusinessRol _businessRol;
        BusinessPermiso _businessPermiso;
        List<EntityRol> _roles;
        List<IPermiso> _permisosExistentes;
        List<IPermiso> _permisosRolElegido;
        List<EntityFamilia> _familias;
        List<EntityPermiso> _permisos;
        EntityRol rolActual;

        public FormGestionRol()
        {
            InitializeComponent();
            _businessRol = new BusinessRol();
            _businessPermiso = new BusinessPermiso();
            cargarRoles();
            setData();
            this.nombre_modulo = "Admin";

            ChangeTranslation();
        }

        private void cargarRoles()
        {
            _roles = _businessRol.BuscarRoles().Data;
            foreach (EntityRol role in _roles)
            {
                role.Permisos = _businessPermiso.getPermisosPorUser(new EntityUser() { Rol = role }).Data;
            }
            _permisosExistentes = _businessPermiso.ObtenerPermisos().Data;

            LLenarCmb(cmbRol, _roles, "Nombre");
        }

        private void setData()
        {
            _permisosRolElegido = new List<IPermiso>();
            rolActual = new EntityRol();
            btnCrear.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            txtNombre.Text = "";
            llenarDG_Pantalla();
            cargarRoles();
            ListBox_PermisosRolActual.SelectedIndex = -1;
        }

        private void llenarDG_Pantalla()
        {
            // Obtener las familias que no están en el rol elegido
            var _familias = _permisosExistentes.OfType<EntityFamilia>()
                .Where(f => !_permisosRolElegido.OfType<EntityFamilia>().Any(rf => rf.Id == f.Id))
                .ToList();

            // Obtener los permisos que no están en el rol elegido
            var _permisos = _permisosExistentes.OfType<EntityPermiso>()
                .Where(p => !_permisosRolElegido.OfType<EntityPermiso>().Any(rp => rp.Id == p.Id))
                .ToList();

            // Obtener todos los permisos de las familias en el rol elegido
            var permisosDeFamiliasRolElegido = _permisosRolElegido.OfType<EntityFamilia>()
                .SelectMany(f => f.Permisos)
                .ToList();

            // Filtrar los permisos para excluir los que están en los permisos de las familias del rol elegido
            _permisos = _permisos.Where(p => !permisosDeFamiliasRolElegido.Any(fp => fp.Id == p.Id)).ToList();

            // Filtrar las familias para excluir aquellas que contienen permisos ya presentes en _permisosRolElegido
            _familias = _familias.Where(f =>
                !f.Permisos.Any(fp => permisosDeFamiliasRolElegido.OfType<EntityPermiso>().Any(rp => rp.Id == fp.Id))
            ).ToList();

            _familias = _familias.Where(f =>
               !f.Permisos.Any(fp => _permisosRolElegido.OfType<EntityPermiso>().Any(rp => rp.Id == fp.Id))
           ).ToList();


            LlenarDG(DG_Familias, _familias, new List<string>() { "Id" });
            LlenarDG(DG_Permisos, _permisos, new List<string>() { "Id", "Is_Familia" });
            llenarListBox();
        }

        private void llenarListBox()
        {
            ListBox_PermisosRolActual.DataSource = null;
            ListBox_PermisosRolActual.DataSource = _permisosRolElegido;
            ListBox_PermisosRolActual.DisplayMember = "Nombre";
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

        private void DG_Familias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1) { return; }
            EntityFamilia familiaElegida = DG_Familias.SelectedRows[0].DataBoundItem as EntityFamilia;
            DialogResult result = MessageBox.Show(SearchTraduccion("DialogFamiliaRol"), "", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.No)
            {
                string permisosNombres = String.Join("\n", familiaElegida.Permisos.Select(x => x.Nombre));
                MessageBox.Show(permisosNombres, "List:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (result == DialogResult.Yes)
            {
                _permisosRolElegido.Add(familiaElegida);
                llenarDG_Pantalla();
            }
        }

        private void DG_Permisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1) { return; }
            EntityPermiso permisoElegido = DG_Permisos.SelectedRows[0].DataBoundItem as EntityPermiso;

            _permisosRolElegido.Add(permisoElegido);
            llenarDG_Pantalla();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bool changeName = false;
            if (!string.IsNullOrEmpty(txtNombre.Text)) rolActual.Nombre = txtNombre.Text; changeName = true;
            if (_permisosRolElegido.Count < 1) { MessageBox.Show(SearchTraduccion("Campos_Incompletos")); return; }
            if (_roles.Any(R => R.Nombre == txtNombre.Text)) { MessageBox.Show(SearchTraduccion("NombreRolRepetido")); return; }
            rolActual.Permisos = _permisosRolElegido;
            var response = _businessRol.ModificarRol(rolActual, changeName);
            RevisarRespuestaServicio(response);
            if (response.Ok is true) guardarEventoBitacora("Se modificó un rol", 4);
            llenarDG_Pantalla();
            setData();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            setData();
        }

        private void btnElegirCmb_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnCrear.Enabled = false;

            rolActual = cmbRol.SelectedItem as EntityRol;
            _permisosRolElegido = rolActual.Permisos;
            llenarDG_Pantalla();
        }


        private void ListBox_PermisosRolActual_DoubleClick(object sender, EventArgs e)
        {
            if (ListBox_PermisosRolActual.SelectedIndex is -1) { return; }
            DialogResult result = MessageBox.Show(SearchTraduccion("RemoverSeleccion"), "Confirmación", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                IPermiso permisoElegido = ListBox_PermisosRolActual.SelectedItem as IPermiso;
                _permisosRolElegido.Remove(permisoElegido);
                llenarDG_Pantalla();
            }
            return;
        }


        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var response = _businessRol.EliminarRol(rolActual);
                RevisarRespuestaServicio(response);
                if (response.Ok is true) { guardarEventoBitacora("Se elimnó un rol", 3); }
                setData();
            }
            return;
        }

        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            if (_roles.Any(R => R.Nombre == txtNombre.Text)) { MessageBox.Show(SearchTraduccion("NombreRolRepetido")); return; }
            if (_permisosRolElegido.Count < 1) { MessageBox.Show(SearchTraduccion("Campos_Incompletos")); return; }
            if (string.IsNullOrEmpty(txtNombre.Text)) { MessageBox.Show("Ingrese un nombre", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var response = _businessRol.AgregarRol(_permisosRolElegido, txtNombre.Text);
            RevisarRespuestaServicio(response);
            if (response.Ok is true) guardarEventoBitacora("Creación de un rol", 3);
            llenarDG_Pantalla();
            setData();
        }
    }
}

