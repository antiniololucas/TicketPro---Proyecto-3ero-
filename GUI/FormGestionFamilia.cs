using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormGestionFamilia : ServiceForm
    {
        BusinessPermiso _businessPermiso;
        List<EntityRol> _roles;
        List<IPermiso> _permisosExistentes;
        List<IPermiso> _permisosFamiliaElegida;
        List<EntityFamilia> _familias;
        List<EntityPermiso> _permisos;
        EntityFamilia _familiaActual;
        public FormGestionFamilia()
        {
            InitializeComponent();
            _businessPermiso = new BusinessPermiso();
            setData();
            ChangeTranslation();
            this.nombre_modulo = "Admin";
        }

        private void setData()
        {
            _permisosExistentes = _businessPermiso.ObtenerPermisos().Data;
            LLenarCmb(cmbFamilias, (List<EntityFamilia>)_businessPermiso.ObtenerPermisos().Data.OfType<EntityFamilia>().ToList(), "Nombre");
            _permisosFamiliaElegida = new List<IPermiso>();
            _familiaActual = new EntityFamilia();
            btnCrear.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            txtNombre.Text = "";
            llenarDG_Pantalla();
            ListBox_PermisosFamiliaActual.SelectedIndex = -1;
        }

        private void llenarDG_Pantalla()
        {
            // Obtener las familias que no están en la familia elegida
            var _familias = _permisosExistentes.OfType<EntityFamilia>()
                .Where(f => !_permisosFamiliaElegida.OfType<EntityFamilia>().Any(rf => rf.Id == f.Id))
                .ToList();

            // Obtener los permisos que no están en la familia elegida
            var _permisos = _permisosExistentes.OfType<EntityPermiso>()
                .Where(p => !_permisosFamiliaElegida.OfType<EntityPermiso>().Any(rp => rp.Id == p.Id))
                .ToList();

            // Obtener todos los permisos de las familias en la familia elegida
            var permisosDeFamiliasRolElegido = _permisosFamiliaElegida.OfType<EntityFamilia>()
                .SelectMany(f => f.Permisos)
                .ToList();

            // Filtrar los permisos para excluir los que están en los permisos de las familias del rol elegido
            _permisos = _permisos.Where(p => !permisosDeFamiliasRolElegido.Any(fp => fp.Id == p.Id)).ToList();

            // Filtrar las familias para excluir aquellas que contienen permisos ya presentes en _permisosRolElegido
            _familias = _familias.Where(f =>
                !f.Permisos.Any(fp => permisosDeFamiliasRolElegido.OfType<EntityPermiso>().Any(rp => rp.Id == fp.Id))
            ).ToList();

            _familias = _familias.Where(f =>
                !f.Permisos.Any(fp => _permisosFamiliaElegida.OfType<EntityPermiso>().Any(rp => rp.Id == fp.Id))
            ).ToList();

            LlenarDG(DG_Familias, _familias, new List<string>() { "Id" });
            LlenarDG(DG_Permisos, _permisos, new List<string>() { "Id", "Is_Familia" });
            llenarListBox();
        }

        private void llenarListBox()
        {
            ListBox_PermisosFamiliaActual.DataSource = null;
            ListBox_PermisosFamiliaActual.DataSource = _permisosFamiliaElegida;
            ListBox_PermisosFamiliaActual.DisplayMember = "Nombre";
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

        private void DG_Familias_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1) { return; }
            EntityFamilia familiaElegida = DG_Familias.SelectedRows[0].DataBoundItem as EntityFamilia;
            DialogResult result = MessageBox.Show(SearchTraduccion("DialogFamiliaFamilia"), "", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.No)
            {
                string permisosNombres = String.Join("\n", familiaElegida.Permisos.Select(x => x.Nombre));
                MessageBox.Show(permisosNombres, "List:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (result == DialogResult.Yes)
            {
                _permisosFamiliaElegida.Add(familiaElegida);
                llenarDG_Pantalla();
            }
        }

        private void DG_Permisos_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1) { return; }
            EntityPermiso permisoElegido = DG_Permisos.SelectedRows[0].DataBoundItem as EntityPermiso;

            _permisosFamiliaElegida.Add(permisoElegido);
            llenarDG_Pantalla();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_permisosExistentes.OfType<EntityFamilia>().Any(F => F.Nombre == txtNombre.Text))
            { MessageBox.Show(SearchTraduccion("FamiliaRepetida")); return; }
            if (_permisosFamiliaElegida.Count < 1) { MessageBox.Show(SearchTraduccion("Campos_Incompletos")); return; }
            bool changeName = false;
            if (!string.IsNullOrEmpty(txtNombre.Text)) _familiaActual.Nombre = txtNombre.Text; changeName = true;
            _familiaActual.Permisos = _permisosFamiliaElegida;
            var response = _businessPermiso.ModificarFamilia(_familiaActual, changeName);
            RevisarRespuestaServicio(response); //Crear metodo update
            llenarDG_Pantalla();
            setData();
            if (response.Ok) guardarEventoBitacora("Se modificó una familia de permisos", 2);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (_permisosExistentes.OfType<EntityFamilia>().Any(F => F.Nombre == txtNombre.Text))
            { MessageBox.Show("FamiliaRepetida"); return; }
            if (_permisosFamiliaElegida.Count < 1) { MessageBox.Show(SearchTraduccion("Campos_Incompletos")); return; }
            if (string.IsNullOrEmpty(txtNombre.Text)) { MessageBox.Show(SearchTraduccion("Campos_Incompletos"), "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var response = _businessPermiso.RegistrarFamilia(_permisosFamiliaElegida, txtNombre.Text);
            RevisarRespuestaServicio(response);
            setData();
            if (response.Ok) guardarEventoBitacora("Se creó una familia de permisos", 2);
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

            _familiaActual = cmbFamilias.SelectedItem as EntityFamilia;
            _permisosFamiliaElegida = _familiaActual.Permisos;
            llenarDG_Pantalla();
        }

        private void ListBox_PermisosFamiliaActual_DoubleClick(object sender, EventArgs e)
        {
            if (ListBox_PermisosFamiliaActual.SelectedIndex is -1) { return; }
            DialogResult result = MessageBox.Show(SearchTraduccion("RemoverSeleccion"), "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                IPermiso permisoElegido = ListBox_PermisosFamiliaActual.SelectedItem as IPermiso;
                _permisosFamiliaElegida.Remove(permisoElegido);
                llenarDG_Pantalla();
            }
            return;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var response = _businessPermiso.EliminarFamilia(_familiaActual);
                RevisarRespuestaServicio(response);
                if (response.Ok) guardarEventoBitacora("Se eliminó una familia de permisos", 1);
                setData();
            }
            return;
        }


    }
}
