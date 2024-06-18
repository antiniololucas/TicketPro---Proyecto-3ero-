namespace GUI
{
    partial class FormSeleccionarEntradas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSeleccionarEntradas));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.DGEntradas = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.bunifuLabel5 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel3 = new Bunifu.UI.WinForms.BunifuLabel();
            this.DG_Detalles = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.bunifuLabel2 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuPanel1 = new Bunifu.UI.WinForms.BunifuPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnVolver = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.numericCantidad = new System.Windows.Forms.NumericUpDown();
            this.bunifuLabel4 = new Bunifu.UI.WinForms.BunifuLabel();
            this.cmbTipoEntrada = new System.Windows.Forms.ComboBox();
            this.btnRegistrar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            ((System.ComponentModel.ISupportInitialize)(this.DGEntradas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_Detalles)).BeginInit();
            this.bunifuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AllowParentOverrides = false;
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.AutoSize = false;
            this.bunifuLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuLabel1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Bold);
            this.bunifuLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.bunifuLabel1.Location = new System.Drawing.Point(0, 0);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(900, 112);
            this.bunifuLabel1.TabIndex = 17;
            this.bunifuLabel1.Text = "SELECCIONAR ENTRADAS";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // DGEntradas
            // 
            this.DGEntradas.AllowCustomTheming = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.DGEntradas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGEntradas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGEntradas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.DGEntradas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGEntradas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DGEntradas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGEntradas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGEntradas.ColumnHeadersHeight = 40;
            this.DGEntradas.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.DGEntradas.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.DGEntradas.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.DGEntradas.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.DGEntradas.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.DGEntradas.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.DGEntradas.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.DGEntradas.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.DGEntradas.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.DGEntradas.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.DGEntradas.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.DGEntradas.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.DGEntradas.CurrentTheme.Name = null;
            this.DGEntradas.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.DGEntradas.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.DGEntradas.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.DGEntradas.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.DGEntradas.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGEntradas.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGEntradas.EnableHeadersVisualStyles = false;
            this.DGEntradas.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.DGEntradas.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.DGEntradas.HeaderBgColor = System.Drawing.Color.Empty;
            this.DGEntradas.HeaderForeColor = System.Drawing.Color.White;
            this.DGEntradas.Location = new System.Drawing.Point(229, 139);
            this.DGEntradas.Name = "DGEntradas";
            this.DGEntradas.ReadOnly = true;
            this.DGEntradas.RowHeadersVisible = false;
            this.DGEntradas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGEntradas.RowTemplate.Height = 40;
            this.DGEntradas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGEntradas.Size = new System.Drawing.Size(650, 249);
            this.DGEntradas.TabIndex = 18;
            this.DGEntradas.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            this.DGEntradas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGEntradas_CellContentClick);
            // 
            // bunifuLabel5
            // 
            this.bunifuLabel5.AllowParentOverrides = false;
            this.bunifuLabel5.AutoEllipsis = false;
            this.bunifuLabel5.CursorType = null;
            this.bunifuLabel5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bunifuLabel5.ForeColor = System.Drawing.SystemColors.Control;
            this.bunifuLabel5.Location = new System.Drawing.Point(12, 139);
            this.bunifuLabel5.Name = "bunifuLabel5";
            this.bunifuLabel5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel5.Size = new System.Drawing.Size(95, 15);
            this.bunifuLabel5.TabIndex = 20;
            this.bunifuLabel5.Text = "Tipos de Entradas:";
            this.bunifuLabel5.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel5.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel3
            // 
            this.bunifuLabel3.AllowParentOverrides = false;
            this.bunifuLabel3.AutoEllipsis = false;
            this.bunifuLabel3.CursorType = null;
            this.bunifuLabel3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bunifuLabel3.ForeColor = System.Drawing.SystemColors.Control;
            this.bunifuLabel3.Location = new System.Drawing.Point(12, 407);
            this.bunifuLabel3.Name = "bunifuLabel3";
            this.bunifuLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel3.Size = new System.Drawing.Size(48, 15);
            this.bunifuLabel3.TabIndex = 27;
            this.bunifuLabel3.Text = "Cantidad";
            this.bunifuLabel3.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel3.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // DG_Detalles
            // 
            this.DG_Detalles.AllowCustomTheming = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.DG_Detalles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DG_Detalles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DG_Detalles.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.DG_Detalles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DG_Detalles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DG_Detalles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DG_Detalles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DG_Detalles.ColumnHeadersHeight = 40;
            this.DG_Detalles.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.DG_Detalles.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.DG_Detalles.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.DG_Detalles.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.DG_Detalles.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.DG_Detalles.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.DG_Detalles.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.DG_Detalles.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.DG_Detalles.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.DG_Detalles.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.DG_Detalles.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.DG_Detalles.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.DG_Detalles.CurrentTheme.Name = null;
            this.DG_Detalles.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.DG_Detalles.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.DG_Detalles.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.DG_Detalles.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.DG_Detalles.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DG_Detalles.DefaultCellStyle = dataGridViewCellStyle6;
            this.DG_Detalles.EnableHeadersVisualStyles = false;
            this.DG_Detalles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.DG_Detalles.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.DG_Detalles.HeaderBgColor = System.Drawing.Color.Empty;
            this.DG_Detalles.HeaderForeColor = System.Drawing.Color.White;
            this.DG_Detalles.Location = new System.Drawing.Point(229, 428);
            this.DG_Detalles.Name = "DG_Detalles";
            this.DG_Detalles.ReadOnly = true;
            this.DG_Detalles.RowHeadersVisible = false;
            this.DG_Detalles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DG_Detalles.RowTemplate.Height = 40;
            this.DG_Detalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DG_Detalles.Size = new System.Drawing.Size(650, 216);
            this.DG_Detalles.TabIndex = 30;
            this.DG_Detalles.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            // 
            // bunifuLabel2
            // 
            this.bunifuLabel2.AllowParentOverrides = false;
            this.bunifuLabel2.AutoEllipsis = false;
            this.bunifuLabel2.CursorType = null;
            this.bunifuLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bunifuLabel2.ForeColor = System.Drawing.SystemColors.Control;
            this.bunifuLabel2.Location = new System.Drawing.Point(229, 407);
            this.bunifuLabel2.Name = "bunifuLabel2";
            this.bunifuLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel2.Size = new System.Drawing.Size(95, 15);
            this.bunifuLabel2.TabIndex = 31;
            this.bunifuLabel2.Text = "Detalle de factura:";
            this.bunifuLabel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel2.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuPanel1
            // 
            this.bunifuPanel1.BackgroundColor = System.Drawing.Color.Transparent;
            this.bunifuPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuPanel1.BackgroundImage")));
            this.bunifuPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.bunifuPanel1.BorderRadius = 3;
            this.bunifuPanel1.BorderThickness = 1;
            this.bunifuPanel1.Controls.Add(this.pictureBox2);
            this.bunifuPanel1.Location = new System.Drawing.Point(0, 0);
            this.bunifuPanel1.Name = "bunifuPanel1";
            this.bunifuPanel1.ShowBorders = true;
            this.bunifuPanel1.Size = new System.Drawing.Size(198, 112);
            this.bunifuPanel1.TabIndex = 32;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(198, 112);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // btnVolver
            // 
            this.btnVolver.AllowAnimations = true;
            this.btnVolver.AllowMouseEffects = true;
            this.btnVolver.AllowToggling = false;
            this.btnVolver.AnimationSpeed = 200;
            this.btnVolver.AutoGenerateColors = false;
            this.btnVolver.AutoRoundBorders = false;
            this.btnVolver.AutoSizeLeftIcon = true;
            this.btnVolver.AutoSizeRightIcon = true;
            this.btnVolver.BackColor = System.Drawing.Color.Transparent;
            this.btnVolver.BackColor1 = System.Drawing.Color.DodgerBlue;
            this.btnVolver.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVolver.BackgroundImage")));
            this.btnVolver.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnVolver.ButtonText = "Volver";
            this.btnVolver.ButtonTextMarginLeft = 0;
            this.btnVolver.ColorContrastOnClick = 45;
            this.btnVolver.ColorContrastOnHover = 45;
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnVolver.CustomizableEdges = borderEdges1;
            this.btnVolver.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnVolver.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnVolver.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnVolver.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnVolver.Enabled = false;
            this.btnVolver.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnVolver.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btnVolver.IconMarginLeft = 11;
            this.btnVolver.IconPadding = 10;
            this.btnVolver.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVolver.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnVolver.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnVolver.IconSize = 25;
            this.btnVolver.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.btnVolver.IdleBorderRadius = 1;
            this.btnVolver.IdleBorderThickness = 1;
            this.btnVolver.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.btnVolver.IdleIconLeftImage = null;
            this.btnVolver.IdleIconRightImage = null;
            this.btnVolver.IndicateFocus = false;
            this.btnVolver.Location = new System.Drawing.Point(748, 662);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnVolver.OnDisabledState.BorderRadius = 1;
            this.btnVolver.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnVolver.OnDisabledState.BorderThickness = 1;
            this.btnVolver.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnVolver.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnVolver.OnDisabledState.IconLeftImage = null;
            this.btnVolver.OnDisabledState.IconRightImage = null;
            this.btnVolver.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.btnVolver.onHoverState.BorderRadius = 1;
            this.btnVolver.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnVolver.onHoverState.BorderThickness = 1;
            this.btnVolver.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.btnVolver.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btnVolver.onHoverState.IconLeftImage = null;
            this.btnVolver.onHoverState.IconRightImage = null;
            this.btnVolver.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnVolver.OnIdleState.BorderRadius = 1;
            this.btnVolver.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnVolver.OnIdleState.BorderThickness = 1;
            this.btnVolver.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnVolver.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btnVolver.OnIdleState.IconLeftImage = null;
            this.btnVolver.OnIdleState.IconRightImage = null;
            this.btnVolver.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnVolver.OnPressedState.BorderRadius = 1;
            this.btnVolver.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnVolver.OnPressedState.BorderThickness = 1;
            this.btnVolver.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnVolver.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btnVolver.OnPressedState.IconLeftImage = null;
            this.btnVolver.OnPressedState.IconRightImage = null;
            this.btnVolver.Size = new System.Drawing.Size(131, 35);
            this.btnVolver.TabIndex = 46;
            this.btnVolver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnVolver.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnVolver.TextMarginLeft = 0;
            this.btnVolver.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnVolver.UseDefaultRadiusAndThickness = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // numericCantidad
            // 
            this.numericCantidad.Location = new System.Drawing.Point(12, 428);
            this.numericCantidad.Name = "numericCantidad";
            this.numericCantidad.Size = new System.Drawing.Size(199, 20);
            this.numericCantidad.TabIndex = 47;
            // 
            // bunifuLabel4
            // 
            this.bunifuLabel4.AllowParentOverrides = false;
            this.bunifuLabel4.AutoEllipsis = false;
            this.bunifuLabel4.CursorType = null;
            this.bunifuLabel4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bunifuLabel4.ForeColor = System.Drawing.SystemColors.Control;
            this.bunifuLabel4.Location = new System.Drawing.Point(229, 118);
            this.bunifuLabel4.Name = "bunifuLabel4";
            this.bunifuLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel4.Size = new System.Drawing.Size(103, 15);
            this.bunifuLabel4.TabIndex = 48;
            this.bunifuLabel4.Text = "Presione para elegir";
            this.bunifuLabel4.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel4.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // cmbTipoEntrada
            // 
            this.cmbTipoEntrada.FormattingEnabled = true;
            this.cmbTipoEntrada.Location = new System.Drawing.Point(12, 175);
            this.cmbTipoEntrada.Name = "cmbTipoEntrada";
            this.cmbTipoEntrada.Size = new System.Drawing.Size(199, 21);
            this.cmbTipoEntrada.TabIndex = 49;
            this.cmbTipoEntrada.SelectedIndexChanged += new System.EventHandler(this.cmbTipoEntrada_SelectedIndexChanged);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.AllowAnimations = true;
            this.btnRegistrar.AllowMouseEffects = true;
            this.btnRegistrar.AllowToggling = false;
            this.btnRegistrar.AnimationSpeed = 200;
            this.btnRegistrar.AutoGenerateColors = false;
            this.btnRegistrar.AutoRoundBorders = false;
            this.btnRegistrar.AutoSizeLeftIcon = true;
            this.btnRegistrar.AutoSizeRightIcon = true;
            this.btnRegistrar.BackColor = System.Drawing.Color.Transparent;
            this.btnRegistrar.BackColor1 = System.Drawing.Color.DodgerBlue;
            this.btnRegistrar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRegistrar.BackgroundImage")));
            this.btnRegistrar.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnRegistrar.ButtonText = "Agregar";
            this.btnRegistrar.ButtonTextMarginLeft = 0;
            this.btnRegistrar.ColorContrastOnClick = 45;
            this.btnRegistrar.ColorContrastOnHover = 45;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.btnRegistrar.CustomizableEdges = borderEdges2;
            this.btnRegistrar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRegistrar.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnRegistrar.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnRegistrar.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnRegistrar.Enabled = false;
            this.btnRegistrar.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistrar.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnRegistrar.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btnRegistrar.IconMarginLeft = 11;
            this.btnRegistrar.IconPadding = 10;
            this.btnRegistrar.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrar.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnRegistrar.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnRegistrar.IconSize = 25;
            this.btnRegistrar.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.btnRegistrar.IdleBorderRadius = 1;
            this.btnRegistrar.IdleBorderThickness = 1;
            this.btnRegistrar.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.btnRegistrar.IdleIconLeftImage = null;
            this.btnRegistrar.IdleIconRightImage = null;
            this.btnRegistrar.IndicateFocus = false;
            this.btnRegistrar.Location = new System.Drawing.Point(12, 470);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnRegistrar.OnDisabledState.BorderRadius = 1;
            this.btnRegistrar.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnRegistrar.OnDisabledState.BorderThickness = 1;
            this.btnRegistrar.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnRegistrar.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnRegistrar.OnDisabledState.IconLeftImage = null;
            this.btnRegistrar.OnDisabledState.IconRightImage = null;
            this.btnRegistrar.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.btnRegistrar.onHoverState.BorderRadius = 1;
            this.btnRegistrar.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnRegistrar.onHoverState.BorderThickness = 1;
            this.btnRegistrar.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.btnRegistrar.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.onHoverState.IconLeftImage = null;
            this.btnRegistrar.onHoverState.IconRightImage = null;
            this.btnRegistrar.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnRegistrar.OnIdleState.BorderRadius = 1;
            this.btnRegistrar.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnRegistrar.OnIdleState.BorderThickness = 1;
            this.btnRegistrar.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnRegistrar.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.OnIdleState.IconLeftImage = null;
            this.btnRegistrar.OnIdleState.IconRightImage = null;
            this.btnRegistrar.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnRegistrar.OnPressedState.BorderRadius = 1;
            this.btnRegistrar.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnRegistrar.OnPressedState.BorderThickness = 1;
            this.btnRegistrar.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnRegistrar.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.OnPressedState.IconLeftImage = null;
            this.btnRegistrar.OnPressedState.IconRightImage = null;
            this.btnRegistrar.Size = new System.Drawing.Size(144, 30);
            this.btnRegistrar.TabIndex = 50;
            this.btnRegistrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRegistrar.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnRegistrar.TextMarginLeft = 0;
            this.btnRegistrar.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnRegistrar.UseDefaultRadiusAndThickness = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // FormSeleccionarEntradas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(28)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(900, 709);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.cmbTipoEntrada);
            this.Controls.Add(this.bunifuLabel4);
            this.Controls.Add(this.numericCantidad);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.bunifuPanel1);
            this.Controls.Add(this.bunifuLabel2);
            this.Controls.Add(this.DG_Detalles);
            this.Controls.Add(this.bunifuLabel3);
            this.Controls.Add(this.bunifuLabel5);
            this.Controls.Add(this.DGEntradas);
            this.Controls.Add(this.bunifuLabel1);
            this.Name = "FormSeleccionarEntradas";
            this.Text = "FormSeleccionarEntradas";
            ((System.ComponentModel.ISupportInitialize)(this.DGEntradas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_Detalles)).EndInit();
            this.bunifuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private Bunifu.UI.WinForms.BunifuDataGridView DGEntradas;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel5;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel3;
        private Bunifu.UI.WinForms.BunifuDataGridView DG_Detalles;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel2;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnVolver;
        private System.Windows.Forms.NumericUpDown numericCantidad;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel4;
        private System.Windows.Forms.ComboBox cmbTipoEntrada;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnRegistrar;
    }
}