using BE;
using BLL;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormPlanificacionEvento : ServiceForm
    {

        List<string> gustos = new List<string>();

        public FormPlanificacionEvento(FormGenerarOrdenPago form_volver)
        {
            InitializeComponent();
            Form_volver = form_volver;
            ChangeTranslation();
            _businessEvento = new BusinessEvento();
            gustos = _businessEvento.BuscarGustosMusicales();
            LoadPanels();
        }

        private void LoadPanels()
        {
            Ubicacion ubi = Form_volver.cmbUbicaciones.SelectedItem as Ubicacion;
            panelCampo.Visible = ubi.Campo > 0;
            cantCampo.Maximum = ubi.Campo;
            panelPalco.Visible = ubi.Palco > 0;
            cantPalco.Maximum = ubi.Palco;
            panelGeneral.Visible = ubi.PlateaGeneral > 0;
            cantGeneral.Maximum = ubi.PlateaGeneral;
            panelPreferencial.Visible = ubi.PlateaPreferencial > 0;
            cantPreferencial.Maximum = ubi.PlateaPreferencial;
            EntityEvento even = Form_volver.evento_Actual;
            lblInfoEvento.Text = $"Evento en {even.Ubicacion}.\nEl {even.Fecha.Date}";
            LLenarCmb(txtOrientacion, gustos , null);
        }

        FormGenerarOrdenPago Form_volver;
        BusinessEvento _businessEvento;

        private void ImagenEvento_Click(object sender, EventArgs e)
        {
            // Crear un OpenFileDialog para seleccionar la imagen.
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Leer el archivo seleccionado en un arreglo de bytes.
                    Form_volver.evento_Actual.Imagen = File.ReadAllBytes(ofd.FileName);
                    using (MemoryStream ms = new MemoryStream(Form_volver.evento_Actual.Imagen))
                    {
                        ImagenEvento.Image = Image.FromStream(ms);
                    }
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form_volver.MostrarEvento();
            Form_volver.Show();
            this.Hide();
        }

        private void btnCargarEvento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtArtistaEvento.Text) || string.IsNullOrEmpty(TxtNombreEvento.Text) || string.IsNullOrEmpty(txtHoraEvento.Text) || string.IsNullOrEmpty(TxtDescripcionEvento.Text))
            {
                MessageBox.Show(SearchTraduccion("Campos_Incompletos")); return;
            }
            try
            {
                Form_volver.evento_Actual.Artista = txtArtistaEvento.Text;
                Form_volver.evento_Actual.Horario = TimeSpan.Parse(txtHoraEvento.Text);
                Form_volver.evento_Actual.Descripcion = TxtDescripcionEvento.Text;
                Form_volver.evento_Actual.Nombre = TxtNombreEvento.Text;
                Form_volver.evento_Actual.PublicoObjetivo = txtOrientacion.Text;
            }
            catch
            {
                MessageBox.Show(SearchTraduccion("FormatoIncorrecto")); return;
            }
            panelEntradas.Enabled = true;
            MessageBox.Show(SearchTraduccion("ExitoCrearEvento"));
        }

        private void txtHora_TextChange(object sender, EventArgs e)
        {
            string input = txtHoraEvento.Text.Replace(":", "");

            if (input.Length > 4)
            {
                input = input.Substring(0, 4);
            }

            // Construir la cadena formateada (hh:mm:ss).
            if (input.Length >= 2)
            {
                input = input.Insert(2, ":");
            }

            // Actualizar el TextBox con el nuevo formato.
            txtHoraEvento.Text = input;

            // Mover el cursor al final para que no interfiera con la escritura del usuario.
            txtHoraEvento.SelectionStart = txtHoraEvento.Text.Length;
        }


        private void btnInicio_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                CambiarForm(new FormInicio());
            }
        }

        private void btnCargarEntradas_Click(object sender, EventArgs e)
        {
            if (cantCampo.Value > 0 && precioCampo.Value <= 1000 || cantGeneral.Value > 0 && precioGeneral.Value <= 1000 ||
                cantPreferencial.Value > 0 && precioPreferencial.Value <= 1000 || cantPalco.Value > 0 && precioPalco.Value <= 1000)
            {
                MessageBox.Show(SearchTraduccion("PrecioIncompleto")); return;
            }
            if ((int)cantCampo.Value > 0)
            {
                EntityEntrada entrada = new EntityEntrada()
                {
                    Tipo = "Campo",
                    Costo_Unitario = (double)precioCampo.Value,
                    Cantidad_Disponible = (int)cantCampo.Value,
                };
                Form_volver.entradas.Add(entrada);
            }
            if ((int)cantPalco.Value > 0)
            {
                EntityEntrada entrada = new EntityEntrada()
                {
                    Tipo = "Palco",
                    Costo_Unitario = (double)precioPalco.Value,
                    Cantidad_Disponible = (int)cantPalco.Value,
                };
                Form_volver.entradas.Add(entrada);
            }
            if ((int)cantGeneral.Value > 0)
            {
                EntityEntrada entrada = new EntityEntrada()
                {
                    Tipo = "Platea General",
                    Costo_Unitario = (double)precioGeneral.Value,
                    Cantidad_Disponible = (int)cantGeneral.Value,
                };
                Form_volver.entradas.Add(entrada);
            }
            if ((int)cantPreferencial.Value > 0)
            {
                EntityEntrada entrada = new EntityEntrada()
                {
                    Tipo = "Platea Preferencial",
                    Costo_Unitario = (double)precioPreferencial.Value,
                    Cantidad_Disponible = (int)cantPreferencial.Value,
                };
                Form_volver.entradas.Add(entrada);
            }
            btnAgregar.Enabled = true;
            MessageBox.Show(SearchTraduccion("ExitoCrearEntradas"));
        }
    }
}
