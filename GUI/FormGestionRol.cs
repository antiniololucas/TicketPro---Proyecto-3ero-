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
    public partial class FormGestionRol : ServiceForm
    {

        BusinessRol _businessRol;
        BusinessPermiso _businessPermiso;
        bool modificar;
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
                .SelectMany(f => f.Permisos) // Suponiendo que la clase EntityFamilia tiene una propiedad Permisos
                .ToList();

            // Filtrar los permisos para excluir los que están en los permisos de las familias del rol elegido
            _permisos = _permisos.Where(p => !permisosDeFamiliasRolElegido.Any(fp => fp.Id == p.Id)).ToList();

            // Filtrar las familias para excluir aquellas que contienen permisos ya presentes en _permisosRolElegido
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
            DialogResult result = MessageBox.Show("¿Desea agregar familia al rol? Presione Si. \nSi presiona NO, podrá ver los permisos que tiene la familia", "Confirmación", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.No)
            {
                string permisosNombres = String.Join("\n", familiaElegida.Permisos.Select(x => x.Nombre));
                MessageBox.Show(permisosNombres, "Permisos:", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            rolActual.Permisos = _permisosRolElegido;
            RevisarRespuestaServicio(_businessRol.ModificarRol(rolActual, changeName));
            llenarDG_Pantalla();
            setData();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text)) { MessageBox.Show("Ingrese un nombre", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            RevisarRespuestaServicio(_businessRol.AgregarRol(_permisosRolElegido, txtNombre.Text));
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
            DialogResult result = MessageBox.Show("¿Desea remover este permiso?", "Confirmación", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                IPermiso permisoElegido = ListBox_PermisosRolActual.SelectedItem as IPermiso;
                _permisosRolElegido.Remove(permisoElegido);
                llenarListBox();
            }
            return;
        }


        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea eliminar este rol?", "Confirmación", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                 RevisarRespuestaServicio(_businessRol.EliminarRol(rolActual));
                setData();
            }
            return;
        }
    }
}

