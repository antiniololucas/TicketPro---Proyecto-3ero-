using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormMaestroEventos : ServiceForm
    {
        public FormMaestroEventos()
        {
            InitializeComponent();
            ChangeTranslation();
            _businessEvento = new BusinessEvento();
            this.nombre_modulo = "Maestros";
            CargarData();
        }

        private void CargarData()
        {
            _eventos = _businessEvento.BuscarEventos();
            LlenarDG(DG_Eventos, _eventos, new List<string>() { "Id", "Imagen", "Id_Planificador" });
            lblCantEventos.Text = _eventos.Count.ToString();
            LLenarCmb(txtOrientacion, _businessEvento.BuscarGustosMusicales(), null);
        }

        BusinessEvento _businessEvento;
        List<EntityEvento> _eventos;
        EntityEvento _Evento_Actual;

        private void DG_Eventos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _Evento_Actual = DG_Eventos.SelectedRows[0].DataBoundItem as EntityEvento;
            TxtNombre.Text = _Evento_Actual.Nombre;
            TxtDescripcion.Text = _Evento_Actual.Descripcion;
            txtArtista.Text = _Evento_Actual.Artista;
            txtUbicacion.Text = _Evento_Actual.Ubicacion;
            txtHora.Text = _Evento_Actual.Horario.ToString();
            txtFecha.Value = _Evento_Actual.Fecha;
            txtOrientacion.Text = _Evento_Actual.PublicoObjetivo;
            using (MemoryStream ms = new MemoryStream(_Evento_Actual.Imagen))
            {
                ImagenEvento.Image = Image.FromStream(ms);
            }
        }

        private void btnLimpiarSeleccion_Click(object sender, EventArgs e)
        {
            LimpiarData();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_Evento_Actual == null)
            {
                MessageBox.Show(SearchTraduccion("SelecioneEvento"));
                return;
            }
            _Evento_Actual.Nombre = TxtNombre.Text;
            _Evento_Actual.Descripcion = TxtDescripcion.Text;
            _Evento_Actual.Artista = txtArtista.Text;
            _Evento_Actual.Ubicacion = txtUbicacion.Text;
            _Evento_Actual.Horario = Convert.ToDateTime(txtHora.Text).TimeOfDay;
            _Evento_Actual.Fecha = txtFecha.Value;
            _Evento_Actual.PublicoObjetivo = txtOrientacion.Text;
            var response = _businessEvento.ModificarEvento(_Evento_Actual);
            RevisarRespuestaServicio(response);
            if (response.Ok is true)
            {
                guardarEventoBitacora("Modificacion de Evento", 3);
                UpdateDigitoVerificador();
                CargarData(); LimpiarData();
            }
        }

        private void ImagenEvento_Click(object sender, EventArgs e)
        {
            // Crear un OpenFileDialog para seleccionar la imagen.
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Leer el archivo seleccionado en un arreglo de bytes.
                    _Evento_Actual.Imagen = File.ReadAllBytes(ofd.FileName);
                    using (MemoryStream ms = new MemoryStream(_Evento_Actual.Imagen))
                    {
                        ImagenEvento.Image = Image.FromStream(ms);
                    }
                }
            }
        }

        private void LimpiarData()
        {
            TxtNombre.Text = " ";
            TxtDescripcion.Text = "";
            txtArtista.Text = "";
            txtUbicacion.Text = "";
            txtHora.Text = "";
            txtFecha.Value = DateTime.Now;
            ImagenEvento.Image = null;
            _Evento_Actual = null;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void lblImagen_Click(object sender, EventArgs e)
        {

        }

        private void lblDescripcion_Click(object sender, EventArgs e)
        {

        }

        private void lblArtista_Click(object sender, EventArgs e)
        {

        }

        private void lblUbicacion_Click(object sender, EventArgs e)
        {

        }

        private void lblHorario_Click(object sender, EventArgs e)
        {

        }

        private void lblFecha_Click(object sender, EventArgs e)
        {

        }

        private void txtUbicacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtHora_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtArtista_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
