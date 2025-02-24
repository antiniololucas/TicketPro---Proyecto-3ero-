﻿using BE;
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

        protected void LlenarDG<T>(DataGridView DG, List<T> list, List<string> columnasOcultas)
        {
            DG.DataSource = null;
            DG.DataSource = list;
            if (columnasOcultas != null)
            {
                foreach (string col in columnasOcultas)
                {
                    DG.Columns[$"{col}"].Visible = false;
                }
            }
        }

        protected void LLenarCmb<T>(ComboBox cmb , List<T> list , string Display)
        {
            cmb.DataSource = list;
            cmb.DisplayMember = Display;
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

        protected void CambiarForm(ServiceForm form)
        {
            form.Show();
            this.Close();
        }

    }
}
