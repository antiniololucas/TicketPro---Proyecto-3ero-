using BE;
using BLL;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormCobrarFactura : ServiceForm
    {
        public FormCobrarFactura()
        {
            InitializeComponent();
            ObtenerFacturasSinCobrar();
            LlenarDG(DG_Facturas, _facturas, new List<string> { "Id", "Id_Cliente" , "Is_Cobrada" });
        }

        BusinessFactura _businessFactura = new BusinessFactura();
        BusinessCliente _businessCliente = new BusinessCliente();
        List<EntityFactura> _facturas;
        EntityFactura facturaActual;

        private void ObtenerFacturasSinCobrar()
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

        private void btnRegistarCliente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtFechaVto.Text)) { MostrarLabelError("Campo Incompleto", lblErrorFecha); return; }
            if (string.IsNullOrEmpty(txtNumTarjeta.Text)) { MostrarLabelError("Campo Incompleto", lblErrorNumero); return; }
            if (string.IsNullOrEmpty(txtNombreTitular.Text)) { MostrarLabelError("Campo Incompleto", lblErrorNombre); return; }

            if (!RegexValidation.IsValidCardNumber(txtNumTarjeta.Text)) { MostrarLabelError("Formato Incorrecto", lblErrorNumero); return; }
            if (!validarFecha()) { MostrarLabelError("Formato Incorrecto", lblErrorFecha); return; }
            if (!RegexValidation.IsValidName(txtNombreTitular.Text)){ MostrarLabelError("Formato Incorrecto", lblErrorNombre); return; }

            DialogResult result = MessageBox.Show($"¿Está seguro que quiere cobrar la factura de {facturaActual.DNI_Cliente}?", "Confirmación", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }
            facturaActual.Is_Cobrada = true;
            var response = _businessFactura.ModificarFactura(facturaActual, true);
            RevisarRespuestaServicio(response);
            if (response.Ok) { FormCobrarFactura frm = new FormCobrarFactura(); frm.Show(); this.Close(); }
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

        private void btnInicio_Click(object sender, EventArgs e)
        {
            FormInicio frm = new FormInicio();
            frm.Show();
            this.Close();
        }
    }
}
