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
    public partial class FormGenerarOrdenPago : ServiceForm
    {
        public FormGenerarOrdenPago()
        {
            InitializeComponent();
            ChangeTranslation();

            this.nombre_modulo = "PlanificacionEvento";

            businessEvento = new BusinessEvento();
            businessEntrada = new BusinessEntrada();
            businessCliente = new BusinessCliente();

            _eventos_Creados = businessEvento.BuscarEventos();
            _asociado_posibles = businessCliente.BuscarClientes().Where(A => A.Is_Planificador is true).ToList();

            LLenarCmb(cmbUbicaciones, Ubicaciones.Ubicaciones_General, "Nombre");
            LlenarDG(DG_Asociados, _asociado_posibles, new List<string> { "Id", "Mail", "Is_Planificador", "Is_Paga" });
            LLenarCmb(cmbClientes, _asociado_posibles, "DNI");
        }

        //Business Usadas
        BusinessEvento businessEvento;
        BusinessCliente businessCliente;
        BusinessEntrada businessEntrada;

        //Listas Acumulativas
        List<EntityEvento> _eventos_Creados;
        List<EntityCliente> _asociado_posibles;

        //Elementos componentes del evento a crear
        EntityCliente asociado_actual = null;
        public List<EntityEntrada> entradas = new List<EntityEntrada>();
        public EntityEvento evento_Actual = new EntityEvento();

        private void btnInicio_Click(object sender, EventArgs e)
        {
            CambiarForm(new FormInicio());
        }

        private void btnVerificarDisponibilidad_Click(object sender, EventArgs e)
        {
            if (txtFecha.Value <= DateTime.Today || txtFecha.Value <= DateTime.Today.AddDays(30))
            {
                MessageBox.Show(SearchTraduccion("FechaInvalidaShow"));
                return;
            }
            EntityEvento eventoOcupado = _eventos_Creados.FirstOrDefault(x => x.Ubicacion == cmbUbicaciones.SelectedItem.ToString() && x.Fecha.Date == txtFecha.Value.Date);
            if (eventoOcupado != null)
            {
                MessageBox.Show(SearchTraduccion("FechaUbiOcupadas") + eventoOcupado.Nombre);
                return;
            }
            MessageBox.Show(SearchTraduccion("DisponibilidadEvento"));
            evento_Actual.Fecha = txtFecha.Value.Date;
            evento_Actual.Ubicacion = cmbUbicaciones.SelectedItem.ToString();
            PanelVerificaDispo.Visible = false;
            PanelAsociado.Visible = true;

            FormPlanificacionEvento frm = new FormPlanificacionEvento(this);
            frm.Show();
            this.Hide();
        }

        public void MostrarEvento()
        {
            panelSeleccionEvento.Visible = true;
            lblNombreEvento.Text = evento_Actual.Nombre.ToUpper();
            lblArtistaMostrar.Text = evento_Actual.Artista;
            LblDescripcionEvento.Text = evento_Actual.Descripcion;
            lblUbicacionMostrar.Text = evento_Actual.Ubicacion;
            lblFechaMostrar.Text = evento_Actual.Fecha.ToString();
            lblHorarioMostrar.Text = evento_Actual.Horario.ToString();
            using (MemoryStream ms = new MemoryStream(evento_Actual.Imagen))
            {
                lblImagen.Image = Image.FromStream(ms);
            }
        }

        private void btnAsociarCliente_Click(object sender, EventArgs e)
        {
            asociado_actual = cmbClientes.SelectedItem as EntityCliente;
            if (asociado_actual != null)
            {
                lblAsociadoPlan.Text = (asociado_actual.Nombre.ToString() + " " + asociado_actual.Apellido);
                btnGenerarOrdenPago.Enabled = true;
            }
        }

        private void btnGenerarOrdenPago_Click(object sender, EventArgs e)
        {
            var response = businessEvento.AgregarEvento(evento_Actual, asociado_actual);

            if (response.Ok)
            {
                foreach (var item in entradas)
                {
                    item.Id_Evento = response.Data;
                    businessEntrada.AgregarEntrada(item);
                }
            }
            RevisarRespuestaServicio(response);
            if (response.Ok)
            {
                guardarEventoBitacora("Generacion Orden de Pago", 3);
                UpdateDigitoVerificador();
               
                // Obtener la lista de clientes que fueron enviados
                var clientes = businessEvento.EnviarPromocionMail(evento_Actual);

                // Obtener la lista de clientes existentes
                List<EntityCliente> clientes_existentes = businessCliente.BuscarClientes();

                // Filtrar clientes existentes cuyos Id estén en la lista de clientes
                clientes_existentes = clientes_existentes.Where(x => clientes.Any(y => y == x.Id)).ToList();

                MailSender.EnviarCorreos(evento_Actual , clientes_existentes);

                CambiarForm(new FormInicio());

            }
        }

        private void btnRegistarAsociado_Click(object sender, EventArgs e)
        {
            FormRegistrarAsociado frm = new FormRegistrarAsociado(this);
            frm.Show(this);
            this.Hide();
        }
    }
}
