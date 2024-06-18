using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormSeleccionarEntradas : ServiceForm
    {
        public FormSeleccionarEntradas(EntityEvento eventoActual, FormGenerarFactura formVolver)
        {
            InitializeComponent();
            entradas = _businessEntrada.BuscarEntradas(eventoActual);
            LlenarDG(DGEntradas, entradas, new List<string>() { "Id", "Id_Evento" });
            LLenarCmb(cmbTipoEntrada, entradas, "Tipo");
            _formVolver = formVolver;
        }

        FormGenerarFactura _formVolver;
        BusinessEntrada _businessEntrada = new BusinessEntrada();
        EntityEntrada entradaActual;
        List<EntityEntrada> entradas;

        private void DGEntradas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            entradaActual = DGEntradas.SelectedRows[0].DataBoundItem as EntityEntrada;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            LlenarDG(_formVolver.DG_FacturaActual, _formVolver.detalles, new List<string>() { "Id_Entrada", "Id_Detalle", "Id_Factura" });
            _formVolver.Show();
            this.Close();
        }

        private void bntnRegistrar_Click(object sender, EventArgs e)
        {
            if (entradaActual is null) { MessageBox.Show("Elija una entrada"); return; }

            if(numericCantidad.Value == 0) { MessageBox.Show("Elija cantidad de entradas"); return; }

            _formVolver.detalles.Add(new EntityDetalle_Factura() { Id_Entrada = entradaActual.Id, Cantidad_Entradas = Convert.ToInt32(numericCantidad.Value) , Tipo_Entrada = entradaActual.Tipo, Costo_parcial = (Convert.ToInt32(numericCantidad.Value) * entradaActual.Costo_Unitario) });
            LlenarDG(_formVolver.DG_FacturaActual, _formVolver.detalles, new List<string>() { "Id_Entrada", "Id_Detalle", "Id_Factura" });
            btnVolver.Enabled = true;
        }

        private void cmbTipoEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            entradaActual = cmbTipoEntrada.SelectedItem as EntityEntrada;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (entradaActual is null) { MessageBox.Show("Elija una entrada"); return; }
            if (numericCantidad.Value == 0) { MessageBox.Show("Elija cantidad de entradas"); return; }

            _formVolver.detalles.Add(new EntityDetalle_Factura() { Id_Entrada = entradaActual.Id, Cantidad_Entradas = Convert.ToInt32(numericCantidad.Value), Tipo_Entrada = entradaActual.Tipo, Costo_parcial = (Convert.ToInt32(numericCantidad.Value) * entradaActual.Costo_Unitario) });
            LlenarDG(DG_Detalles, _formVolver.detalles, new List<string>() { "Id_Entrada", "Id_Detalle", "Id_Factura" });
            btnVolver.Enabled = true;
        }
    }
}
