using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormPagarOrdenAsociado : ServiceForm
    {
        public FormPagarOrdenAsociado()
        {
            InitializeComponent();
            ChangeTranslation();
            llenarData();
            this.nombre_modulo = "PlanificarEvento";
        }

        private void llenarData()
        {
            _eventos = _businessEvento.BuscarEventos().Where(E => E.Is_Paga is false).ToList();
            _Asociados = _businessCliente.BuscarClientes().Where(C => C.Is_Planificador is true).ToList();
        }

        BusinessCliente _businessCliente = new BusinessCliente();
        BusinessEvento _businessEvento = new BusinessEvento();
        List<EntityEvento> _eventos;
        List<EntityCliente> _Asociados;
        EntityEvento evento_a_acobrar;

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            EntityCliente Asociado = _Asociados.FirstOrDefault(C => C.DNI == TxtDniAsociado.Value);

            if (Asociado is null)
            {
                MessageBox.Show(SearchTraduccion("NoPoseeEvento"));
                return;
            }

            List<EntityEvento> evento = _eventos.FindAll(E => E.Id_Planificador == Asociado.Id && E.Fecha <= DateTime.Today).ToList();

            if (evento.Count <= 0)
            {
                MessageBox.Show(SearchTraduccion("NoPoseeEvento"));
                return;
            }

            LlenarDG(DG_Ordenes, evento, new List<string>() { "Id", "Is_Paga", "Id_Planificador", "Imagen", "Horario" });
        }

        private void btnCobrarFactura_Click(object sender, EventArgs e)
        {
            if (evento_a_acobrar is null)
            {
                MessageBox.Show(SearchTraduccion("EventoVACIO"));
            }
            if (txtCbu.Value.ToString().Length == 0 || string.IsNullOrEmpty(TxtAlias.Text) || string.IsNullOrEmpty(txtNombreTitular.Text))
            {
                MessageBox.Show(SearchTraduccion("Campos_Incompletos"));
            }
            evento_a_acobrar.Is_Paga = true;
            var response = _businessEvento.ModificarEvento(evento_a_acobrar);

            if (response.Ok)
            {
                MessageBox.Show(SearchTraduccion("OrdenPagada"));
                guardarEventoBitacora("Se abonó un evento", 4);
                UpdateDigitoVerificador();
                CambiarForm(new FormInicio());
            }
        }

        private void DG_Ordenes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            evento_a_acobrar = DG_Ordenes.SelectedRows[0].DataBoundItem as EntityEvento;
            double total = _businessEvento.BuscarMonto(evento_a_acobrar.Id);
            if(total == 0)
            {
                MessageBox.Show(SearchTraduccion("MontoInsuficiente"));
                return;
            }
            panelCobrar.Enabled = true;
            lblTotal.Visible = true;
            lblTotal.Text = "Total de la factura " + total.ToString();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }
    }
}
