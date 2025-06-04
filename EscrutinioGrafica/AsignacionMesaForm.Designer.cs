namespace EscrutinioGrafica
{
    partial class AsignacionMesaForm
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
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbLocalidad = new System.Windows.Forms.ComboBox();
            this.btnAsignacionMesa = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lstCandidatos = new System.Windows.Forms.ListBox();
            this.txtNumeroMesa = new System.Windows.Forms.TextBox();
            this.txtVotantesAsignados = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(97, 72);
            this.dtpFecha.MaxDate = new System.DateTime(2025, 5, 26, 0, 0, 0, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha.TabIndex = 0;
            this.dtpFecha.Value = new System.DateTime(2025, 5, 26, 0, 0, 0, 0);
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(97, 115);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(200, 21);
            this.cmbLocalidad.TabIndex = 1;
            // 
            // btnAsignacionMesa
            // 
            this.btnAsignacionMesa.Location = new System.Drawing.Point(96, 145);
            this.btnAsignacionMesa.Name = "btnAsignacionMesa";
            this.btnAsignacionMesa.Size = new System.Drawing.Size(114, 38);
            this.btnAsignacionMesa.TabIndex = 3;
            this.btnAsignacionMesa.Text = "Asignar mesa";
            this.btnAsignacionMesa.UseVisualStyleBackColor = true;
            this.btnAsignacionMesa.Click += new System.EventHandler(this.btnAsignacionMesa_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Fecha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Localidad:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.Controls.Add(this.btnAsignacionMesa);
            this.panel1.Location = new System.Drawing.Point(12, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 218);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(398, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Número de mesa asignado:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Número de votantes asignados:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(398, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Lista de candidatos:";
            // 
            // lstCandidatos
            // 
            this.lstCandidatos.FormattingEnabled = true;
            this.lstCandidatos.Location = new System.Drawing.Point(507, 115);
            this.lstCandidatos.Name = "lstCandidatos";
            this.lstCandidatos.Size = new System.Drawing.Size(156, 108);
            this.lstCandidatos.TabIndex = 10;
            // 
            // txtNumeroMesa
            // 
            this.txtNumeroMesa.Enabled = false;
            this.txtNumeroMesa.Location = new System.Drawing.Point(561, 44);
            this.txtNumeroMesa.Name = "txtNumeroMesa";
            this.txtNumeroMesa.Size = new System.Drawing.Size(57, 20);
            this.txtNumeroMesa.TabIndex = 11;
            // 
            // txtVotantesAsignados
            // 
            this.txtVotantesAsignados.Enabled = false;
            this.txtVotantesAsignados.Location = new System.Drawing.Point(561, 75);
            this.txtVotantesAsignados.Name = "txtVotantesAsignados";
            this.txtVotantesAsignados.Size = new System.Drawing.Size(57, 20);
            this.txtVotantesAsignados.TabIndex = 12;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(291, 254);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(114, 38);
            this.btnCerrar.TabIndex = 13;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // AsignacionMesaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 304);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.txtVotantesAsignados);
            this.Controls.Add(this.txtNumeroMesa);
            this.Controls.Add(this.lstCandidatos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLocalidad);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.panel1);
            this.Name = "AsignacionMesaForm";
            this.Text = "Asignación de mesa:";
            this.Load += new System.EventHandler(this.AsignacionMesaForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ComboBox cmbLocalidad;
        private System.Windows.Forms.Button btnAsignacionMesa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lstCandidatos;
        private System.Windows.Forms.TextBox txtNumeroMesa;
        private System.Windows.Forms.TextBox txtVotantesAsignados;
        private System.Windows.Forms.Button btnCerrar;
    }
}