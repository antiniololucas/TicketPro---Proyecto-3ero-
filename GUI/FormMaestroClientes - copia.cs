﻿using BE;
using BLL;
using Services;
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
    public partial class FormMaestroClientes : ServiceForm
    {
        public FormMaestroClientes()
        {
            InitializeComponent();
            _clientes = _businessCliente.BuscarClientes();
            ActualizarInformacion();
        }

        BusinessCliente _businessCliente = new BusinessCliente();
        EntityCliente _clienteActual = new EntityCliente();
        List<EntityCliente> _clientes;
        bool isEncripted = false;

        private void btnInicio_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

        private void ActualizarInformacion()
        {
            LlenarDG(DG_Clientes, _clientes, new List<string>() { "Id" });
            lblCantClientes.Text = _clientes.Count.ToString(); 
        }

        private void DG_Clientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1) { return; }
            TxtNombre.Text = DG_Clientes.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            TxtApellido.Text = DG_Clientes.Rows[e.RowIndex].Cells["Apellido"].Value.ToString();
            TxtDni.Text = DG_Clientes.Rows[e.RowIndex].Cells["Dni"].Value.ToString();
            TxtMail.Text = DG_Clientes.Rows[e.RowIndex].Cells["Mail"].Value.ToString();
            _clienteActual = DG_Clientes.SelectedRows[0].DataBoundItem as EntityCliente;
            btnModificar.Enabled = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var response = _businessCliente.ModificarCliente(_clienteActual.Id ,TxtDni.Text, TxtNombre.Text, TxtApellido.Text, TxtMail.Text);
            RevisarRespuestaServicio(response);
            if (response.Ok) 
            {
                _clientes = _businessCliente.BuscarClientes();
                isEncripted = false;
                ActualizarInformacion();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            TxtNombre.Text = string.Empty;
            TxtApellido.Text = string.Empty;
            TxtDni.Text = string.Empty;
            TxtMail.Text = string.Empty;
            btnModificar.Enabled = false;
        }

        private void btnChangeEncrypt_Click(object sender, EventArgs e)
        {
            if (isEncripted)
            {
                foreach (var item in _clientes)
                {
                    item.Mail = CryptoManager.ReversibleEncrypt(item.Mail);
                }
                isEncripted = false;
            }
            else
            {
                foreach (var item in _clientes)
                {
                    item.Mail = CryptoManager.ReversibleDecrypt(item.Mail);
                }
                isEncripted = true;
            }
            ActualizarInformacion();
        }
    }
}
