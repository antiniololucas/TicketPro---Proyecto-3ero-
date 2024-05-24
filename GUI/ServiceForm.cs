using BLL;
using Bunifu.UI.WinForms;
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
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
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

        protected void EsconderLabelError(List<BunifuLabel> ListLbl)
        {
            foreach (BunifuLabel lbl in ListLbl)
            {
                lbl.Visible = false;
            }
        }

        protected void MostrarLabelError(string mensaje, BunifuLabel label)
        {
            label.Text = mensaje;
            label.Visible = true;
        }

        protected void RevisarRespuestaServicio<T>(BusinessResponse<T> respuesta)
        {
            if (!respuesta.Ok)
            {
                MessageBox.Show(respuesta.Mensaje, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (respuesta.Ok && !string.IsNullOrEmpty(respuesta.Mensaje))
            {
                MessageBox.Show(respuesta.Mensaje, "Great!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
