namespace JunoSystem
{
    partial class frmJunoSistema
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnInserir = new System.Windows.Forms.Button();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.radInserir = new System.Windows.Forms.RadioButton();
            this.radAlterar = new System.Windows.Forms.RadioButton();
            this.radSolicitar = new System.Windows.Forms.RadioButton();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkGoogleDrive = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.label1.Location = new System.Drawing.Point(22, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            this.label1.UseWaitCursor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.lblStatus.Location = new System.Drawing.Point(80, 334);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "..........";
            this.lblStatus.UseWaitCursor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnInserir
            // 
            this.btnInserir.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnInserir.Location = new System.Drawing.Point(26, 268);
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Size = new System.Drawing.Size(117, 39);
            this.btnInserir.TabIndex = 2;
            this.btnInserir.Text = "Insert Device";
            this.btnInserir.UseVisualStyleBackColor = true;
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // txtDevice
            // 
            this.txtDevice.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDevice.Location = new System.Drawing.Point(26, 42);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(266, 22);
            this.txtDevice.TabIndex = 3;
            // 
            // txtLatitude
            // 
            this.txtLatitude.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLatitude.Location = new System.Drawing.Point(26, 85);
            this.txtLatitude.MaxLength = 50;
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(266, 22);
            this.txtLatitude.TabIndex = 4;
            // 
            // txtLongitude
            // 
            this.txtLongitude.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLongitude.Location = new System.Drawing.Point(26, 129);
            this.txtLongitude.MaxLength = 50;
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(266, 22);
            this.txtLongitude.TabIndex = 5;
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDevice.Location = new System.Drawing.Point(23, 25);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(51, 17);
            this.lblDevice.TabIndex = 6;
            this.lblDevice.Text = "Device";
            // 
            // lblLatitude
            // 
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblLatitude.Location = new System.Drawing.Point(23, 68);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(59, 17);
            this.lblLatitude.TabIndex = 7;
            this.lblLatitude.Text = "Latitude";
            // 
            // lblLongitude
            // 
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblLongitude.Location = new System.Drawing.Point(23, 112);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(71, 17);
            this.lblLongitude.TabIndex = 8;
            this.lblLongitude.Text = "Longitude";
            // 
            // radInserir
            // 
            this.radInserir.AutoSize = true;
            this.radInserir.Cursor = System.Windows.Forms.Cursors.Default;
            this.radInserir.Location = new System.Drawing.Point(22, 223);
            this.radInserir.Name = "radInserir";
            this.radInserir.Size = new System.Drawing.Size(68, 21);
            this.radInserir.TabIndex = 9;
            this.radInserir.TabStop = true;
            this.radInserir.Text = "Inserir";
            this.radInserir.UseVisualStyleBackColor = true;
            this.radInserir.Click += new System.EventHandler(this.radInserir_Click);
            // 
            // radAlterar
            // 
            this.radAlterar.AutoSize = true;
            this.radAlterar.Cursor = System.Windows.Forms.Cursors.Default;
            this.radAlterar.Location = new System.Drawing.Point(119, 223);
            this.radAlterar.Name = "radAlterar";
            this.radAlterar.Size = new System.Drawing.Size(71, 21);
            this.radAlterar.TabIndex = 10;
            this.radAlterar.TabStop = true;
            this.radAlterar.Text = "Alterar";
            this.radAlterar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radAlterar.UseVisualStyleBackColor = true;
            this.radAlterar.Click += new System.EventHandler(this.radAlterar_Click);
            // 
            // radSolicitar
            // 
            this.radSolicitar.AutoSize = true;
            this.radSolicitar.Cursor = System.Windows.Forms.Cursors.Default;
            this.radSolicitar.Location = new System.Drawing.Point(216, 223);
            this.radSolicitar.Name = "radSolicitar";
            this.radSolicitar.Size = new System.Drawing.Size(79, 21);
            this.radSolicitar.TabIndex = 11;
            this.radSolicitar.TabStop = true;
            this.radSolicitar.Text = "Solicitar";
            this.radSolicitar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radSolicitar.UseVisualStyleBackColor = true;
            this.radSolicitar.Click += new System.EventHandler(this.radSolicitar_Click);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblEmail.Location = new System.Drawing.Point(23, 164);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(42, 17);
            this.lblEmail.TabIndex = 13;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtEmail.Location = new System.Drawing.Point(26, 181);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(266, 22);
            this.txtEmail.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.Location = new System.Drawing.Point(149, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 39);
            this.button1.TabIndex = 14;
            this.button1.Text = "Register New Email";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkGoogleDrive
            // 
            this.chkGoogleDrive.AutoSize = true;
            this.chkGoogleDrive.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkGoogleDrive.Location = new System.Drawing.Point(174, 334);
            this.chkGoogleDrive.Name = "chkGoogleDrive";
            this.chkGoogleDrive.Size = new System.Drawing.Size(131, 21);
            this.chkGoogleDrive.TabIndex = 15;
            this.chkGoogleDrive.Text = "My GoogleDrive";
            this.chkGoogleDrive.Click += new System.EventHandler(this.chkGoogleDrive_Click);
            // 
            // frmJunoSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 362);
            this.Controls.Add(this.chkGoogleDrive);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.radSolicitar);
            this.Controls.Add(this.radAlterar);
            this.Controls.Add(this.radInserir);
            this.Controls.Add(this.lblLongitude);
            this.Controls.Add(this.lblLatitude);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.txtLongitude);
            this.Controls.Add(this.txtLatitude);
            this.Controls.Add(this.txtDevice);
            this.Controls.Add(this.btnInserir);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "frmJunoSistema";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Juno System";
            this.Load += new System.EventHandler(this.frmJunoSistema_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnInserir;
        private System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.RadioButton radInserir;
        private System.Windows.Forms.RadioButton radAlterar;
        private System.Windows.Forms.RadioButton radSolicitar;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkGoogleDrive;
    }
}

