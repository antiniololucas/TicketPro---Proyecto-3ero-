using BE;
using BLL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormInicio : ServiceForm
    {
        private readonly SessionManager _sessionManager = SessionManager.GetInstance();
        private BusinessPermiso _businessPermiso;

        public FormInicio()
        {
            InitializeComponent();
            _businessPermiso = new BusinessPermiso();
            _businesEvento = new BusinessEvento();
            eventos = _businesEvento.BuscarEventos().Where(x => x.Fecha > DateTime.Today).OrderBy(x => x.Fecha).Take(3).ToList();
            validateRol();
            ChangeTranslation();
            UpdateEventoMostrado();
            this.nombre_modulo = "Admin";
        }

        private void validateRol()
        {

            PanelBtnVenta.Visible = _sessionManager.HasPermission(1);
            PanelBtnCobranza.Visible = _sessionManager.HasPermission(2);
            PanelBtnUsuario.Visible = _sessionManager.HasPermission(3);
            PanelBtnMaestros.Visible = _sessionManager.HasPermission(4);
            PanelBtnReporte.Visible = _sessionManager.HasPermission(5);
            PanelBtnAdmin.Visible = _sessionManager.HasPermission(7);
            PánelBtnAyuda.Visible = _sessionManager.HasPermission(8);
            PanelBtnPlanificar.Visible = _sessionManager.HasPermission(18);
        }

        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            PanelSubMenuAdmin.Visible = !PanelSubMenuAdmin.Visible;
        }

        private void BtnUsuario_Click(object sender, EventArgs e)
        {
            PanelSubMenuUsuario.Visible = !PanelSubMenuUsuario.Visible;
        }

        private void btnVentaEntradas_Click(object sender, EventArgs e)
        {
            PanelSubMenuEntradas.Visible = !PanelSubMenuEntradas.Visible;
        }

        private void btnCobranza_Click(object sender, EventArgs e)
        {
            panelSubMenuCobranza.Visible = !panelSubMenuCobranza.Visible;
        }

        private void btnMaestros_Click(object sender, EventArgs e)
        {
            panelSubMenuMaestros.Visible = !panelSubMenuMaestros.Visible;
        }

        private void btnCobranza_Click_1(object sender, EventArgs e)
        {
            panelSubMenuCobranza.Visible = !panelSubMenuCobranza.Visible;
        }

        private void btnReporteClick(object sender, EventArgs e)
        {
            panelSubMenuReportes.Visible = !panelSubMenuReportes.Visible;
        }

        private void btnPlanificarEvento_Click(object sender, EventArgs e)
        {
            subPanelPlanificar.Visible = !subPanelPlanificar.Visible;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar la sesión?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.No) { return; }
            guardarEventoBitacora("Logout", 1);
            SessionManager.Logout();
            CambiarForm(new FormInicioSesion());
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormCambiarClave());
        }

        private void btnGestionUsuario_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormGestionUsuario());
        }

        private void BtnRegistrarCliente_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormRegistrarCliente());
        }

        private void btnGenerarFactura_Click_1(object sender, EventArgs e)
        {
            CambiarForm(new FormGenerarFactura());
        }

        private void btnMaestroClientes_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormMaestroClientes());
        }

        private void btnCobrarFactura_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormCobrarFactura());
        }

        private void btnMaestrosCliente_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormMaestroClientes());
        }

        private void BtnAyuda_Click(object sender, EventArgs e)
        {
            string pdfPath = Path.Combine(Application.StartupPath, "Resources", "GUIA DE AYUDA AL USUARIO.pdf");
            string desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "GUIA DE AYUDA AL USUARIO.pdf");

            if (File.Exists(pdfPath))
            {
                // Copia el archivo al escritorio si no existe allí
                if (!File.Exists(desktopPath))
                {
                    File.Copy(pdfPath, desktopPath);
                }

                // Abre el archivo PDF en la ruta original
                System.Diagnostics.Process.Start(pdfPath);
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGestionPefiles_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormGestionRol());
        }

        private void btnGestionFamilias_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormGestionFamilia());
        }



        private void btn_bitacoraEventos_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormBitacoraEventos());
        }

        private void btn_RestoreInicio_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormBackupRestore());
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormMaestroEventos());
        }

        private void btnBitacoraCambios_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormAuditarCambios());
        }

        private void btnReportFactura_Click(object sender, EventArgs e)
        {
            BusinessFactura businessFactura = new BusinessFactura();
            DataTable dt = businessFactura.ObtenerInforme();
            string nombreArchivo = SearchTraduccion("NombreSalidaInforme");
            PDFGenerator.SaveToExcel(dt,nombreArchivo);
            MessageBox.Show(SearchTraduccion("ExcelCompleto") + nombreArchivo + ".xlsx");
        }

        private void btnGenerarOrdenPagoMenu_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormGenerarOrdenPago());
        }

        private void btnRegistrarAsociadoGeneral_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormRegistrarAsociado());
        }

        private void btnPagarOrdenAsociado_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormPagarOrdenAsociado());
        }

        private void btnReportEventos_Click(object sender, EventArgs e)
        {
            BusinessEvento businessFactura = new BusinessEvento();
            DataTable dt = businessFactura.ObtenerInforme();
            string nombreArchivo = SearchTraduccion("NombreSalidaInformeEvento");
            PDFGenerator.SaveToExcel(dt,nombreArchivo);
            MessageBox.Show(SearchTraduccion("ExcelCompleto") + nombreArchivo + ".xlsx");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateEventoMostrado();
        }

        BusinessEvento _businesEvento;
        List<EntityEvento> eventos;
        int counter = 0;

        private void UpdateEventoMostrado()
        {
            if (eventos.Count == 0)
            {
                return; //Foto IA, lbl FALTA POCO cambio de texto y anular timers
            }
            using (MemoryStream ms = new MemoryStream(eventos[counter].Imagen))
            {
                pictureBoxEvento.Image = Image.FromStream(ms);
            }
            lblEventoActual.Text = eventos[counter].Nombre + " " + eventos[counter].Artista + " " + eventos[counter].Fecha;
            counter = counter == eventos.Count-1 ? 0 : counter + 1;
        }

        private int colorIndex = 0;
        private Color[] colors = { Color.Black, Color.DarkBlue, Color.White, Color.LightGray, Color.DarkGray };
        private void timer2_Tick(object sender, EventArgs e)
        {
            lblFaltaPoco.ForeColor = colors[colorIndex];
            colorIndex = (colorIndex + 1) % colors.Length;
        }
    }
}


