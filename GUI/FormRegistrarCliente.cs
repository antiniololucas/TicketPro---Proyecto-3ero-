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
        }

        BusinessCliente _businessCliente = new BusinessCliente();
        

        public FormRegistrarCliente(FormGenerarFactura formGenerarFactura)
        {
            InitializeComponent();
            _formVolver = formGenerarFactura;
            btnVolver.Visible = true;
            btnInicio.Visible = false;
        }

        private void btnRegistarCliente_Click(object sender, EventArgs e)
        {
           var response = _businessCliente.RegistrarCliente(txtDni.Text, txtNombre.Text, txtApellido.Text, txtMail.Text);
           RevisarRespuestaServicio(response);
            if (response.Ok)
            {
                _formVolver.clienteActual = response.Data;
                _formVolver.lblCliente.Text = "Factura actual de: " + (response.Data.Nombre + response.Data.Apellido);
                List<EntityCliente> clientes = _businessCliente.BuscarClientes();
                LlenarDG(_formVolver.DG_Clientes, clientes, new List<string>() { "ID" });
                LLenarCmb(_formVolver.cmbClientes, clientes, "DNI");
                _formVolver.cmbClientes.SelectedItem = clientes.Last();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            _formVolver.Show();
            this.Close();
        }
    }
}
