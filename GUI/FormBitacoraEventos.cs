using BE;
using BLL;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormBitacoraEventos : ServiceForm
    {
        public FormBitacoraEventos()
        {
            InitializeComponent();
            ChangeTranslation();
            _businessEventoBitacora = new BusinessEventoBitacora();
            _businessUser = new BusinessUser();
            cargarFiltros();
            completar_DG();
            this.nombre_modulo = "Bitacora de Eventos";
        }

        private void cargarFiltros()
        {
            cmbEvento.DataSource = _businessEventoBitacora.selectTiposEvento();
            cmbModulos.DataSource = _businessEventoBitacora.selectModulos();
            cmbUsuarios.DataSource = _businessUser.GetUsers();
            cmbUsuarios.DisplayMember = "Username";
            LimpiarInput();
        }

        private void LimpiarInput()
        {
            cmbEvento.SelectedIndex = -1;
            cmbModulos.SelectedIndex = -1;
            cmbUsuarios.SelectedIndex = -1;
            cmbCriticidad.SelectedIndex = -1;
            dateDesde.Value = (DateTime.Now.AddDays(-2));
            dateHasta.Value = DateTime.Now;
            txtNombre.Text = "";
            txtApellido.Text = "";
            _eventos = _businessEventoBitacora.buscarEventosBitacora(0, (DateTime.Now.AddDays(-2)), DateTime.Now, 0).Data;
        }

        private BusinessEventoBitacora _businessEventoBitacora;
        private BusinessUser _businessUser;
        private List<EntityEventoBitacora> _eventos;

        public void completar_DG()
        {
            LlenarDG(DG_EventosBitacora, _eventos, new List<string>() { "Id" });
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

        private void btnAplicarFiltro_Click(object sender, EventArgs e)
        {
            if (dateDesde.Value > dateHasta.Value)
            {
                MessageBox.Show(SearchTraduccion("Error"));
                dateDesde.Value = DateTime.Now.AddDays(-2);
                return;
            }
            int usuarioId = (cmbUsuarios.SelectedItem as EntityUser)?.Id ?? 0;
            DateTime fechaInicio = dateDesde.Value;
            DateTime fechaFin = dateHasta.Value;
            int criticidad = (cmbCriticidad.SelectedItem != null) ? Convert.ToInt32(cmbCriticidad.SelectedItem) : 0;
            string modulo = string.IsNullOrEmpty((string)cmbModulos.SelectedItem) ? null : (string)cmbModulos.SelectedItem;
            string evento = string.IsNullOrEmpty((string)cmbEvento.SelectedItem) ? null : (string)cmbEvento.SelectedItem;

            _eventos = _businessEventoBitacora.buscarEventosBitacora(
                usuarioId,
                fechaInicio,
                fechaFin,
                criticidad,
                modulo,
                evento
            ).Data;
            completar_DG();
        }

        private void DG_EventosBitacora_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EntityEventoBitacora _Evento_Actual = DG_EventosBitacora.SelectedRows[0].DataBoundItem as EntityEventoBitacora;
            txtApellido.Text = _Evento_Actual.Login.Apellido;
            txtNombre.Text = _Evento_Actual.Login.Nombre;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            DataTable dataTable = DataGridViewToDataTable(DG_EventosBitacora);

            if (dataTable.Rows.Count == 0)
            {
                // Mostrar error
                RevisarRespuestaServicio(new BusinessResponse<bool>(false, false, "MessageNoHayDatosGenerarReporteBitacoraEventos"));
                return;
            }

            guardarEventoBitacora("PDF de bitacora", 4);

            // Crear un documento PDF
            PdfDocument pdfDocument = new PdfDocument();
            pdfDocument.Info.Title = "Informe bitacora Eventos";//SearchTraduccion("LblBitacoraEventos");

            // Crear una página en el documento PDF en formato horizontal (landscape)
            PdfPage pdfPage = pdfDocument.AddPage();
            pdfPage.Width = XUnit.FromMillimeter(297); // Ancho en milímetros para formato A4 horizontal
            pdfPage.Height = XUnit.FromMillimeter(210); // Alto en milímetros para formato A4 horizontal
            XGraphics graphics = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 10, XFontStyleEx.Regular);
            XPen borderPen = new XPen(XColors.Black, 0.5);

            // Definir márgenes y altura de la celda
            double margin = 20;
            double cellHeight = 20;
            double xStart = margin;
            double yStart = margin;
            double pageWidth = pdfPage.Width.Point - margin * 2;
            double pageHeight = pdfPage.Height.Point - margin * 2;

            // Definir el ancho fijo para la columna "Criticidad" (ajustar según sea necesario)
            double criticidadWidth = 60;
            double remainingWidth = pageWidth - criticidadWidth;
            double numberOfColumns = dataTable.Columns.Count - 1; // Excluyendo la columna "Criticidad"
            double columnWidth = numberOfColumns > 0 ? remainingWidth / numberOfColumns : 0;

            // Dibujar el encabezado de la tabla con fondo gris y bordes
            graphics.DrawRectangle(XBrushes.LightGray, xStart, yStart, pageWidth, cellHeight);

            // Dibujar encabezados de columnas
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.ColumnName == "Criticidad")
                {
                    graphics.DrawRectangle(borderPen, xStart, yStart, criticidadWidth, cellHeight);
                    graphics.DrawString(column.ColumnName, font, XBrushes.Black, new XRect(xStart + 5, yStart + 5, criticidadWidth, cellHeight), XStringFormats.TopLeft);
                    xStart += criticidadWidth;
                }
                else
                {
                    graphics.DrawRectangle(borderPen, xStart, yStart, columnWidth, cellHeight);
                    graphics.DrawString(column.ColumnName, font, XBrushes.Black, new XRect(xStart + 5, yStart + 5, columnWidth, cellHeight), XStringFormats.TopLeft);
                    xStart += columnWidth;
                }
            }

            yStart += cellHeight;
            xStart = margin; // Reinicia la posición horizontal para las celdas de datos

            // Dibujar las filas de la tabla con bordes
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName == "Criticidad")
                    {
                        graphics.DrawRectangle(borderPen, xStart, yStart, criticidadWidth, cellHeight);
                        graphics.DrawString(row[column].ToString(), font, XBrushes.Black, new XRect(xStart + 5, yStart + 5, criticidadWidth, cellHeight), XStringFormats.TopLeft);
                        xStart += criticidadWidth;
                    }
                    else
                    {
                        graphics.DrawRectangle(borderPen, xStart, yStart, columnWidth, cellHeight);
                        graphics.DrawString(row[column].ToString(), font, XBrushes.Black, new XRect(xStart + 5, yStart + 5, columnWidth, cellHeight), XStringFormats.TopLeft);
                        xStart += columnWidth;
                    }
                }
                yStart += cellHeight;
                xStart = margin; // Reinicia la posición horizontal para la siguiente fila

                // Verifica si la página se ha llenado
                if (yStart + cellHeight > pdfPage.Height.Point - margin)
                {
                    pdfPage = pdfDocument.AddPage();
                    pdfPage.Width = XUnit.FromMillimeter(297); // Ancho en milímetros para formato A4 horizontal
                    pdfPage.Height = XUnit.FromMillimeter(210); // Alto en milímetros para formato A4 horizontal
                    graphics = XGraphics.FromPdfPage(pdfPage);
                    yStart = margin;
                    xStart = margin;

                    // Redefinir los tamaños de columna para la nueva página
                    pageWidth = pdfPage.Width.Point - margin * 2;
                    remainingWidth = pageWidth - criticidadWidth;
                    columnWidth = numberOfColumns > 0 ? remainingWidth / numberOfColumns : 0;
                }
            }

            // Guardar el PDF en el escritorio
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = $"InformeBitacora{DateTime.Now.ToString("yyyy_MM_dd_hh_mm")}.pdf";
            //$"{SearchTraduccion("NombreArchivoBitacora")}_{DateTime.Now.ToString("yyyy_MM_dd_hh_mm")}.pdf";
            string filePath = Path.Combine(desktopPath, fileName);
            pdfDocument.Save(filePath);

            // Abrir el archivo PDF
            Process.Start(filePath);
        }

        private void btnLimpiarBtc_Click(object sender, EventArgs e)
        {
            LimpiarInput();
            completar_DG();
        }
    }
}

