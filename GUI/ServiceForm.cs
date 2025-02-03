using BE;
using BLL;
using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuButton;
using Services;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class ServiceForm : Form, IObserver
    {
        protected readonly BusinessIdioma _businessIdioma;
        protected readonly BusinessEventoBitacora _businessEventoBitacora;
        protected SessionManager _sessionManager;
        protected string nombre_modulo;
        private BusinessDigitoVerificador _businessDigitoVerificador;

        public ServiceForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            _sessionManager = SessionManager.GetInstance();

            _businessEventoBitacora = new BusinessEventoBitacora();
            _businessIdioma = new BusinessIdioma();
            _businessDigitoVerificador = new BusinessDigitoVerificador();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(900, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "TicketPro";
            this.ResumeLayout(false);
        }

        public void LlenarDG<T>(DataGridView dgv, List<T> data, List<string> columnsToHide)
        {
            dgv.DataSource = null;
            dgv.DataSource = data;

            if (columnsToHide != null)
            {
                foreach (var column in columnsToHide)
                {
                    if (dgv.Columns[column] != null)
                    {
                        dgv.Columns[column].Visible = false;
                    }
                }
            }

            dgv.ClearSelection();
        }


        protected void LLenarCmb<T>(ComboBox cmb, List<T> list, string Display)
        {
            cmb.DataSource = list;
            if (Display != null) cmb.DisplayMember = Display;
        }

        protected void EsconderLabelError(List<BunifuLabel> ListLbl)
        {
            foreach (BunifuLabel lbl in ListLbl)
            {
                lbl.Visible = false;
            }
        }

        protected void MostrarLabelError(BunifuLabel label)
        {
            label.Visible = true;
        }

        protected void RevisarRespuestaServicio<T>(BusinessResponse<T> respuesta, EntityIdioma idioma = null)
        {
            if (!respuesta.Ok)
            {
                MessageBox.Show(SearchTraduccion(respuesta.Mensaje, idioma), "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (respuesta.Ok && !string.IsNullOrEmpty(respuesta.Mensaje))
            {
                MessageBox.Show(SearchTraduccion(respuesta.Mensaje, idioma), "Great!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected void CambiarForm(ServiceForm form)
        {
            form.Show();
            this.Close();
        }

        public void Notify(EntityIdioma idioma)
        {
            ChangeTranslation(idioma);
        }

        protected void ChangeTranslation(EntityIdioma idioma = null, Control.ControlCollection collectionPanel = null)
        {
            if (idioma == null) idioma = _sessionManager.Idioma;

            Control.ControlCollection controlCollection = collectionPanel ?? Controls;

            foreach (Control item in controlCollection)
            {
                if (item is Panel || item is BunifuPanel) ChangeTranslation(idioma, item.Controls);

                if (item is Label || item is BunifuLabel || item is Button || item is BunifuButton || item is CheckBox)
                {
                    // Buscar por name en la tabla junto con el id y setear el text del idioma seleccionado
                    item.Text = _businessIdioma.GetTraduccion(idioma, item.Name).Data;
                }
            }
        }

        protected string SearchTraduccion(string controlName, EntityIdioma idioma = null)
        {
            return _businessIdioma.GetTraduccion(idioma ?? _sessionManager.Idioma, controlName).Data;
        }

        protected void guardarEventoBitacora(string evento, int criticidad)
        {
            _sessionManager = SessionManager.GetInstance();
            int user = _sessionManager.User.Id;
            var response = _businessEventoBitacora.guardarEventoBitacora(user, this.nombre_modulo, evento, criticidad);
            if (!response.Ok) { MessageBox.Show("Error con la bitacora de eventos"); }
        }

        // Método para convertir DataGridView a DataTable
        protected DataTable DataGridViewToDataTable(DataGridView dataGridView)
        {
            DataTable dataTable = new DataTable();

            // Agregar columnas al DataTable
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dataTable.Columns.Add(column.HeaderText);
            }

            // Agregar filas al DataTable
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        dataRow[i] = row.Cells[i].Value;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            if (dataTable.Columns.Contains("Id"))
            {
                dataTable.Columns.Remove("Id");
            }

            return dataTable;
        }

        protected bool ValidarDigitoVerificador()
        {
            EntityDigitoVerificador digitoVerificadorCalculado = _businessDigitoVerificador.CalcularDigitoVerificador().Data;
            EntityDigitoVerificador digitoVerificadorDB = _businessDigitoVerificador.BuscarDigitoVerificador().Data;

            return string.Equals(digitoVerificadorCalculado.DVH, digitoVerificadorDB.DVH)
                && string.Equals(digitoVerificadorCalculado.DVV, digitoVerificadorDB.DVV);
        }

        protected void UpdateDigitoVerificador()
        {
            _businessDigitoVerificador.ActualizarDigitoVerificador();
        }
    }
}
