﻿using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormSeleccionarEntradas : ServiceForm
    {
        public FormSeleccionarEntradas(EntityEvento eventoActual, FormGenerarFactura formVolver)
        {
            InitializeComponent();
            ChangeTranslation();
            entradas = _businessEntrada.BuscarEntradas(eventoActual);
            if (entradas.All(f => f.Cantidad_Disponible == 0))
            {
                MessageBox.Show(SearchTraduccion("NoDisponibilidad"));
            }
            LlenarDG(DGEntradas, entradas, new List<string>() { "Id", "Id_Evento" });
            LLenarCmb(cmbTipoEntrada, entradas, "Tipo");
            _formVolver = formVolver;
            ChangeTranslation();
            this.nombre_modulo = "Ventas";
        }

        FormGenerarFactura _formVolver;
        BusinessEntrada _businessEntrada = new BusinessEntrada();
        EntityEntrada entradaActual;
        List<EntityEntrada> entradas;

        private void DGEntradas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            entradaActual = DGEntradas.SelectedRows[0].DataBoundItem as EntityEntrada;
            numericCantidad.Maximum = entradaActual.Cantidad_Disponible;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            LlenarDG(_formVolver.DG_FacturaActual, _formVolver.detalles, new List<string>() { "Id_Entrada", "Id_Detalle", "Id_Factura" });
            _formVolver.Show();
            this.Close();
        }

        private void bntnRegistrar_Click(object sender, EventArgs e)
        {
            if (entradaActual is null) { MessageBox.Show("ElijaEntrada"); return; }

            if (numericCantidad.Value == 0) { MessageBox.Show(SearchTraduccion("Campos_incompletos")); return; }

            _formVolver.detalles.Add(new EntityDetalle_Factura() { Id_Entrada = entradaActual.Id, Cantidad_Entradas = Convert.ToInt32(numericCantidad.Value), Tipo_Entrada = entradaActual.Tipo, Costo_parcial = (Convert.ToInt32(numericCantidad.Value) * entradaActual.Costo_Unitario) });

            LlenarDG(_formVolver.DG_FacturaActual, _formVolver.detalles, new List<string>() { "Id_Entrada", "Id_Detalle", "Id_Factura" });
            btnVolverSeleccion.Enabled = true;
        }

        private void cmbTipoEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            entradaActual = cmbTipoEntrada.SelectedItem as EntityEntrada;
            numericCantidad.Maximum = entradaActual.Cantidad_Disponible;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (_formVolver.detalles.Exists(f => f.Id_Entrada == entradaActual.Id)) { MessageBox.Show(SearchTraduccion("Error")); return; }
            if (entradaActual is null) { MessageBox.Show("ElijaEntrada"); return; }
            if (numericCantidad.Value == 0) { MessageBox.Show("Campos_Incompletos"); return; }

            _formVolver.detalles.Add(new EntityDetalle_Factura() { Id_Entrada = entradaActual.Id, Cantidad_Entradas = Convert.ToInt32(numericCantidad.Value), Tipo_Entrada = entradaActual.Tipo, Costo_parcial = (Convert.ToInt32(numericCantidad.Value) * entradaActual.Costo_Unitario) });
            LlenarDG(DG_Detalles, _formVolver.detalles, new List<string>() { "Id_Entrada", "Id_Detalle", "Id_Factura" });
            btnVolverSeleccion.Enabled = true;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(SearchTraduccion("ConfirmacionSeguro"), "", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }
            CambiarForm(new FormInicio());
        }

        private void DG_Detalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EntityDetalle_Factura detalle = DG_Detalles.SelectedRows[0].DataBoundItem as EntityDetalle_Factura;
            DialogResult result = MessageBox.Show(SearchTraduccion("RemoverSeleccion"), "Confirmación", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }
            _formVolver.detalles.Remove(detalle);
            LlenarDG(DG_Detalles, _formVolver.detalles, new List<string>() { "Id_Entrada", "Id_Detalle", "Id_Factura" });
            if (_formVolver.detalles.Count == 0)
            {
                btnVolverSeleccion.Enabled = false;
            }
        }
    }
}
