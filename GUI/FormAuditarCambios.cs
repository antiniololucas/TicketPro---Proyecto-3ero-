using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormAuditarCambios : ServiceForm
    {
        public FormAuditarCambios()
        {
            InitializeComponent();
            ChangeTranslation();
            _businessEvento = new BusinessEvento();
            LimpiarData();
            cargarData();
        }

        BusinessEvento _businessEvento;
        EntityEvento_C _evento_actual;
        List<EntityEvento_C> _eventos_c;

        private void cargarData()
        {
            _eventos_c = _businessEvento.BuscarEventos_C();
            LlenarDG(DG_Eventos_C, _eventos_c, new List<string>() { "Id_Planificador" });
        }

        private void LimpiarData()
        {
            txtId.Text = "";
            TxtNombre.Text = "";
            txtArtista.Text = "";
            dateDesde.Value = DateTime.Today.AddDays(-30);
            dateHasta.Value = DateTime.Today;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

        private void DG_Eventos_C_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _evento_actual = DG_Eventos_C.SelectedRows[0].DataBoundItem as EntityEvento_C;
            txtId.Text = _evento_actual.Id.ToString();
            TxtNombre.Text = _evento_actual.Nombre;
            txtArtista.Text = _evento_actual.Artista;
            dateDesde.Value = _evento_actual.Fecha_Modificacion.AddDays(-365);
            dateHasta.Value = _evento_actual.Fecha_Modificacion;
        }

        private void btnLimpiarSeleccion_Click(object sender, EventArgs e)
        {
            LimpiarData();
            cargarData();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            _eventos_c = _businessEvento.BuscarEventos_C();
            if (dateDesde.Value > dateHasta.Value)
            {
                MessageBox.Show(SearchTraduccion("Error"));
                dateDesde.Value = DateTime.Now.AddDays(-2);
                return;
            }
            if (txtId.Text.Length > 0)
            {
                _eventos_c = _eventos_c.Where(x => x.Id.ToString() == txtId.Text).ToList();
            }
            if (TxtNombre.Text.Length > 0)
            {
                _eventos_c = _eventos_c.Where(x => x.Nombre == TxtNombre.Text).ToList();
            }
            if (txtArtista.Text.Length > 0)
            {
                _eventos_c = _eventos_c.Where(x => x.Artista == txtArtista.Text).ToList();
            }
            _eventos_c = _eventos_c.Where(x => x.Fecha_Modificacion.Date <= dateHasta.Value.Date && x.Fecha_Modificacion.Date >= dateDesde.Value.Date).ToList();
            LlenarDG(DG_Eventos_C, _eventos_c, null);
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (_evento_actual is null || _evento_actual.Act is true)
            {
                MessageBox.Show(SearchTraduccion("ErrorSeleccioneCambio"));
                return;
            }
            EntityEvento evento_restaurado = new EntityEvento()
            {
                Id = _evento_actual.Id,
                Nombre = _evento_actual.Nombre,
                Artista = _evento_actual.Artista,
                Ubicacion = _evento_actual.Ubicacion,
                Descripcion = _evento_actual.Descripcion,
                Fecha = _evento_actual.Fecha,
                Horario = _evento_actual.Horario,
                Imagen = _evento_actual.Imagen,
                Id_Planificador = _evento_actual.Id_Planificador,
                Is_Paga = false
            };
            RevisarRespuestaServicio(_businessEvento.ModificarEvento(evento_restaurado));
            cargarData();
        }
    }
}
