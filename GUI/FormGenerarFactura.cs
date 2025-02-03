using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormGenerarFactura : ServiceForm
    {
        public FormGenerarFactura()
        {
            InitializeComponent();
            CargaInicio();
            ChangeTranslation();
            this.nombre_modulo = "Ventas";
        }

        private void CargaInicio()
        {
            _eventos = _businessEvento.BuscarEventos().Where(E => E.Fecha > DateTime.Now && E.Is_Paga is false).ToList();
            _clientes = (List<EntityCliente>)_businessCliente.BuscarClientes().Where(C => C.Is_Planificador is false).ToList();
            LlenarDG(DG_Eventos, _eventos, new List<string>() { "Id", "Descripcion", "Horario", "Artista", "Imagen", "Act", "Fecha_Modificacion", "Is_Paga" });
            LlenarDG(DG_Clientes, _clientes, new List<string>() { "ID", "Mail", "Is_Planificador" });
            LLenarCmb(cmbEventos, _eventos, "Nombre");
            LLenarCmb(cmbClientes, _clientes, "DNI");
        }

        BusinessEvento _businessEvento = new BusinessEvento();
        BusinessCliente _businessCliente = new BusinessCliente();
        BusinessFactura _businessFactura = new BusinessFactura();

        List<EntityEvento> _eventos;
        List<EntityCliente> _clientes;
        EntityEvento eventoActual;
        EntityCliente clienteActual;
        public List<EntityDetalle_Factura> detalles = new List<EntityDetalle_Factura>();

        private void MostrarDetalleEvento()
        {
            panelSeleccionEvento.Visible = true;
            lblNombreEvento.Text = eventoActual.Nombre.ToUpper();
            lblArtista.Text = eventoActual.Artista;
            LblDescripcionEvento.Text = eventoActual.Descripcion;
            lblUbicacion.Text = eventoActual.Ubicacion;
            lblFecha.Text = eventoActual.Fecha.ToString();
            lblHorario.Text = eventoActual.Horario.ToString();
            using (MemoryStream ms = new MemoryStream(eventoActual.Imagen))
            {
                lblImagen.Image = Image.FromStream(ms);
            }
        }

        private void btnBuscarEvento_Click(object sender, EventArgs e)
        {

            EntityEvento evento = cmbEventos.SelectedItem as EntityEvento;
            if (evento == null) { MessageBox.Show("Seleccione evento existente"); return; }
            eventoActual = evento;
            MostrarDetalleEvento();

        }

        private void btnVerEntradas_Click(object sender, EventArgs e)
        {
            FormSeleccionarEntradas frm = new FormSeleccionarEntradas(eventoActual, this);
            frm.Show();
            this.Hide();
            lblFacturaActual.Text = $"Factura actual de {eventoActual.Nombre}";
            panelSeleccionEvento.Visible = false;
            PanelCliente.Visible = true;
            btnBuscarEvento.Visible = false;
        }

        private void DG_Eventos_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (detalles.Count != 0) { return; }
            eventoActual = DG_Eventos.SelectedRows[0].DataBoundItem as EntityEvento;
            cmbEventos.SelectedItem = eventoActual;
            MostrarDetalleEvento();
        }

        private void btnAsociarCliente_Click(object sender, EventArgs e)
        {
            if (clienteActual != null)
            {
                lblCliente.Text = (clienteActual.Nombre.ToString() + " " + clienteActual.Apellido);
                btnGenerarFactura.Enabled = true;
            }
        }

        private void DG_Clientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            clienteActual = DG_Clientes.SelectedRows[0].DataBoundItem as EntityCliente;
        }

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            clienteActual = cmbClientes.SelectedItem as EntityCliente;
        }

        private void btnRegistarCliente_Click(object sender, EventArgs e)
        {
            FormRegistrarCliente frm = new FormRegistrarCliente(this);
            frm.Show();
            this.Hide();
        }

        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($" {SearchTraduccion("preguntaConfirmacion1")} {clienteActual.DNI} {SearchTraduccion("preguntaConfirmacion2")}  {eventoActual.Nombre}?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }

            double monto = 0;
            detalles.ForEach(d => monto += d.Costo_parcial);

            EntityFactura factura = new EntityFactura()
            {
                Id_Cliente = clienteActual.Id,
                Fecha = DateTime.Now,
                Monto_Total = monto,
                Is_Cobrada = false,
            };
            var response = _businessFactura.RegistrarFactura(factura, detalles);
            RevisarRespuestaServicio(response);
            BusinessEvento businessEvento = new BusinessEvento();
            if (response.Ok)
            {
                guardarEventoBitacora("Creación de Factura", 5);
                businessEvento.AgregarGustoCliente(clienteActual.Id, eventoActual.PublicoObjetivo);
            }
            UpdateDigitoVerificador();
            CambiarForm(new FormInicio());
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }
    }
}
