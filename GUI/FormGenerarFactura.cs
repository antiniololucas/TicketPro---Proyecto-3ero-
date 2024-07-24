using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void CargaInicio()
        {
            _eventos = _businessEvento.BuscarEventos().Where(E => E.Fecha > DateTime.Now).ToList();
            _clientes = _businessCliente.BuscarClientes();
            LlenarDG(DG_Eventos, _eventos, new List<string>() { "Id", "Descripcion", "Horario", "Artista", "Imagen" });
            LlenarDG(DG_Clientes, _clientes, new List<string>() { "ID", "Mail" });
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
            lblCliente.Text =  (clienteActual.Nombre.ToString() + " " + clienteActual.Apellido);
            btnGenerarFactura.Enabled = true;
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
            DialogResult result = MessageBox.Show($" {SearchTraduccion("preguntaConfirmacion1")} { clienteActual.DNI} { SearchTraduccion("preguntaConfirmacion2")}  {eventoActual.Nombre}?", "", MessageBoxButtons.YesNo);
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
            RevisarRespuestaServicio(_businessFactura.RegistrarFactura(factura, detalles));
            CambiarForm(new FormInicio());
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }
    }
}
