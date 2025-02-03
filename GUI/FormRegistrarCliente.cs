using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            ChangeTranslation();
            txtDni.Select();
            this.nombre_modulo = "Ventas";
        }

        BusinessCliente _businessCliente = new BusinessCliente();


        public FormRegistrarCliente(FormGenerarFactura formGenerarFactura)
        {
            InitializeComponent();
            _formVolver = formGenerarFactura;
            btnVolverFactura.Visible = true;
            ChangeTranslation();
        }

        private void btnRegistarCliente_Click(object sender, EventArgs e)
        {
            var response = _businessCliente.RegistrarCliente(txtDni.Text, txtNombre.Text, txtApellido.Text, txtMail.Text, false);
            RevisarRespuestaServicio(response);
            if (_formVolver != null)
            {
                List<EntityCliente> clientes = _businessCliente.BuscarClientes().Where(C => C.Is_Planificador is false).ToList();
                LlenarDG(_formVolver.DG_Clientes, clientes, new List<string>() { "ID" });
                LLenarCmb(_formVolver.cmbClientes, clientes, "DNI");
            }
            if (response.Ok)
            {
                guardarEventoBitacora("Registro de un Cliente", 4);
                UpdateDigitoVerificador();
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
                DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return; }
            }
            CambiarForm(new FormInicio());
        }
    }
}
