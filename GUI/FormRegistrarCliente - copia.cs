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
    public partial class FormRegistrarCliente : ServiceForm
    {
        private FormGenerarFactura _formVolver;

        public FormRegistrarCliente()
        {
            InitializeComponent();
            AcceptButton = btnRegistarCliente;
            txtDni.Select();
        }

        BusinessCliente _businessCliente = new BusinessCliente();
        

        public FormRegistrarCliente(FormGenerarFactura formGenerarFactura)
        {
            InitializeComponent();
            _formVolver = formGenerarFactura;
            btnVolver.Visible = true;
        }

        private void btnRegistarCliente_Click(object sender, EventArgs e)
        {
            var response = _businessCliente.RegistrarCliente(txtDni.Text, txtNombre.Text, txtApellido.Text, txtMail.Text);
            RevisarRespuestaServicio(response);
            if (_formVolver != null)
            {
                List<EntityCliente> clientes = _businessCliente.BuscarClientes();
                LlenarDG(_formVolver.DG_Clientes, clientes, new List<string>() { "ID" });
                LLenarCmb(_formVolver.cmbClientes, clientes, "DNI");
            }
            if (response.Ok)
            {
                txtDni.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtMail.Text = "";
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            _formVolver.Show();
            this.Close();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            if (_formVolver != null)
            {
                DialogResult result = MessageBox.Show($"¿Está seguro que quiere abandonar la carga de factura?", "Confirmación", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return; }
            }
            CambiarForm(new FormInicio());
        }
    }
}
