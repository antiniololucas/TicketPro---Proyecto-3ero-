using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormRegistrarAsociado : ServiceForm
    {
        private BusinessCliente _businessCliente = new BusinessCliente();
        FormGenerarOrdenPago _formVolver;

        public FormRegistrarAsociado()
        {
            InitializeComponent();
            ChangeTranslation();

            this.nombre_modulo = "PlanificarEvento";
        }

        public FormRegistrarAsociado(FormGenerarOrdenPago frm_volver)
        {
            InitializeComponent();
            ChangeTranslation();
            _formVolver = frm_volver;
            btnVolverOrden.Visible = true;
            this.nombre_modulo = "PlanificarEvento";
        }

        private void btnRegistarCliente_Click(object sender, EventArgs e)
        {
            var response = _businessCliente.RegistrarCliente(txtDni.Text, txtNombre.Text, txtApellido.Text, txtMail.Text, true);
            RevisarRespuestaServicio(response);
            if (_formVolver != null)
            {
                List<EntityCliente> clientes = _businessCliente.BuscarClientes().Where(C => C.Is_Planificador is true).ToList();
                LlenarDG(_formVolver.DG_Asociados, clientes, new List<string>() { "ID" });
                LLenarCmb(_formVolver.cmbClientes, clientes, "DNI");
            }
            if (response.Ok)
            {
                guardarEventoBitacora("Registro de un Asociado Planificador", 4);
                UpdateDigitoVerificador();
                txtDni.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtMail.Text = "";
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (_formVolver != null)
            {
                DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return; }
            }
            CambiarForm(new FormInicio());
        }

        private void btnVolverOrden_Click(object sender, EventArgs e)
        {
            _formVolver.Show();
            this.Close();
        }
    }
}

