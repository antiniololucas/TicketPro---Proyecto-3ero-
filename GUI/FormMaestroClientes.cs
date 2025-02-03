using BE;
using BLL;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormMaestroClientes : ServiceForm
    {
        public FormMaestroClientes()
        {
            InitializeComponent();
            ActualizarInformacion();
            ChangeTranslation();
            this.nombre_modulo = "Maestros";
        }

        BusinessCliente _businessCliente = new BusinessCliente();
        EntityCliente _clienteActual = new EntityCliente();
        List<EntityCliente> _clientes;
        bool isEncripted = false;

        private void btnInicio_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

        private void Limpiar()
        {
            TxtNombre.Text = string.Empty;
            TxtApellido.Text = string.Empty;
            TxtDni.Text = string.Empty;
            txtMail.Text = string.Empty;
            btnModificar.Enabled = false;
        }

        private void ActualizarInformacion()
        {
            _clientes = _businessCliente.BuscarClientes().Where(C => C.Is_Planificador is false).ToList();
            LlenarDG(DG_Clientes, _clientes, new List<string>() { "Id", "Is_Planificador" });
            lblCantClientes.Text = _clientes.Count.ToString();
        }

        private void DG_Clientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1) { return; }
            TxtNombre.Text = DG_Clientes.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            TxtApellido.Text = DG_Clientes.Rows[e.RowIndex].Cells["Apellido"].Value.ToString();
            TxtDni.Text = DG_Clientes.Rows[e.RowIndex].Cells["Dni"].Value.ToString();
            txtMail.Text = DG_Clientes.Rows[e.RowIndex].Cells["Mail"].Value.ToString();
            _clienteActual = DG_Clientes.SelectedRows[0].DataBoundItem as EntityCliente;
            btnModificar.Enabled = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var response = _businessCliente.ModificarCliente(_clienteActual.Id, TxtDni.Text, TxtNombre.Text, TxtApellido.Text, txtMail.Text);
            RevisarRespuestaServicio(response);
            if (response.Ok)
            {
                guardarEventoBitacora("Modificación de un cliente", 5);
                UpdateDigitoVerificador();
                isEncripted = false;
                ActualizarInformacion();
                Limpiar();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
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
            LlenarDG(DG_Clientes, _clientes, new List<string>() { "Id", "Is_Planificador" });
        }

        private void btnSerializar_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {

                    // Serializar la lista de servicios a JSON
                    string json = JsonConvert.SerializeObject(_clientes, Newtonsoft.Json.Formatting.Indented);

                    // Construir la ruta completa del archivo JSON
                    string filePath = Path.Combine(folderDialog.SelectedPath, $"{SearchTraduccion("LblServiciosSerializar")}_{DateTime.Now.ToString("yyyy_MM_dd")}.json");

                    // Guardar el archivo JSON en la ruta especificada
                    File.WriteAllText(filePath, json);
                    Process.Start("notepad.exe", filePath);
                    guardarEventoBitacora("Serializacion Clientes", 4);
                    RevisarRespuestaServicio(new BusinessResponse<bool>(true, true, "MessageSerializadoCorrectamente"));
                }
            }

        }

        private void btnDeserializar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON Files (*.json)|*.json";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(openFileDialog.FileName);

                    _clientes = JsonConvert.DeserializeObject<List<EntityCliente>>(json);

                    ActualizarInformacion();

                    guardarEventoBitacora("Serializacion Clientes", 4);
                    RevisarRespuestaServicio(new BusinessResponse<bool>(true, true, "MessageDeserializadoCorrectamente"));
                }
            }

        }
    }
}
