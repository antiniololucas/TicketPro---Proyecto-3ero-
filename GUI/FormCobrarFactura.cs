using BE;
using BLL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormCobrarFactura : ServiceForm
    {
        public FormCobrarFactura()
        {
            InitializeComponent();
            GetFacturasSinCobrarConDNI();
            LlenarDG(DG_Facturas, _facturas, new List<string> { "Id", "Id_Cliente", "Is_Cobrada" });
            AcceptButton = btnCobrarFactura;
            ChangeTranslation();
            this.nombre_modulo = "Cobranza";
        }

        BusinessFactura _businessFactura = new BusinessFactura();
        BusinessCliente _businessCliente = new BusinessCliente();
        BusinessEntrada businessEntrada = new BusinessEntrada();
        BusinessEvento businessEvento = new BusinessEvento();
        List<EntityFactura> _facturas;
        EntityFactura facturaActual;

        private void GetFacturasSinCobrarConDNI()
        {
            _facturas = (List<EntityFactura>)_businessFactura.BuscarFacturas().Data.Where(c => !c.Is_Cobrada).ToList();
            foreach (var fac in _facturas)
            {
                fac.DNI_Cliente = _businessCliente.BuscarClientes().FirstOrDefault(C => C.Id == fac.Id_Cliente).DNI;
            }
        }

        private void TxtFechaVto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtFechaVto_TextChange(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                // Agregar automáticamente la barra '/'
                if (textBox.Text.Length == 2 && !textBox.Text.Contains("/"))
                {
                    textBox.Text = textBox.Text.Insert(2, "/");
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private bool validarFecha()
        {
            if (!RegexValidation.IsValidFechaVto(TxtFechaVto.Text))
            {
                return false;
            }
            string[] parts = TxtFechaVto.Text.Split('/');
            int month = int.Parse(parts[0]);
            int year = int.Parse(parts[1]) + 2000;

            DateTime inputDate = new DateTime(year, month, 1);
            DateTime currentDate = DateTime.Now;

            return inputDate > currentDate ? true : false;
        }

        private void DG_Facturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            facturaActual = DG_Facturas.SelectedRows[0].DataBoundItem as EntityFactura;
            panelCobrar.Enabled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

        private void btnCobrarFactura_Click(object sender, EventArgs e)
        {
            bool hayError = false;

            //Errores inputs vacios.
            if (string.IsNullOrEmpty(TxtFechaVto.Text)) { MostrarLabelError(lblErrorFecha); hayError = true; }
            if (string.IsNullOrEmpty(txtNumTarjeta.Text)) { MostrarLabelError(lblErrorNumero); hayError = true; }
            if (string.IsNullOrEmpty(txtNombreTitular.Text)) { MostrarLabelError(lblErrorNombre); hayError = true; }
            if (hayError) { MessageBox.Show(SearchTraduccion("Campos_Incompletos")); return; }


            //Errores inputs invalidos.
            if (!RegexValidation.IsValidCardNumber(txtNumTarjeta.Text)) { MostrarLabelError(lblErrorNumero); hayError = true; }
            if (!validarFecha()) { MostrarLabelError(lblErrorFecha); hayError = true; }
            if (!RegexValidation.IsValidName(txtNombreTitular.Text)) { MostrarLabelError(lblErrorNombre); hayError = true; }
            if (hayError) { MessageBox.Show(SearchTraduccion("FormatoIncorrecto")); return; }

            //Confirmación de solicitud.
            DialogResult result = MessageBox.Show($"{SearchTraduccion("ConfirmacionCobroFactura")}{facturaActual.DNI_Cliente}?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }

            //Persistencia y cierre de acción.
            facturaActual.Is_Cobrada = true;
            var response = _businessFactura.ModificarFactura(facturaActual, true);
            RevisarRespuestaServicio(response);
            if (response.Ok)
            {
                FormCobrarFactura frm = new FormCobrarFactura(); frm.Show(); this.Close();
                guardarEventoBitacora("Factura Cobrada", 4);
                UpdateDigitoVerificador();
            }
            EntityDetalle_Factura detalle = _businessFactura.getDetalleFactura(facturaActual).Data;
            EntityEntrada entrada = businessEntrada.selectAllEntrads().Find(E => E.Id == detalle.Id_Entrada);
            EntityEvento evento = businessEvento.BuscarEventos().Find(E => E.Id == entrada.Id_Evento);
            PDFGenerator.GeneratePDF(facturaActual, detalle, evento, entrada);
        }
    }
}
