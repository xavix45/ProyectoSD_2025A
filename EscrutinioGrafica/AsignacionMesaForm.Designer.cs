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
            this.label6 = new System.Windows.Forms.Label();
            this.tb_mesaAConsulta = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_LocalidadConsulta = new System.Windows.Forms.ComboBox();
            this.tb_ConsultaMesa = new System.Windows.Forms.TextBox();
            this.btn_Consulta = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(129, 89);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFecha.MaxDate = new System.DateTime(2025, 5, 26, 0, 0, 0, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(265, 22);
            this.dtpFecha.TabIndex = 0;
            this.dtpFecha.Value = new System.DateTime(2025, 5, 26, 0, 0, 0, 0);
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(129, 142);
            this.cmbLocalidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(265, 24);
            this.cmbLocalidad.TabIndex = 1;
            // 
            // btnAsignacionMesa
            // 
            this.btnAsignacionMesa.Location = new System.Drawing.Point(128, 178);
            this.btnAsignacionMesa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAsignacionMesa.Name = "btnAsignacionMesa";
            this.btnAsignacionMesa.Size = new System.Drawing.Size(152, 47);
            this.btnAsignacionMesa.TabIndex = 3;
            this.btnAsignacionMesa.Text = "Asignar mesa";
            this.btnAsignacionMesa.UseVisualStyleBackColor = true;
            this.btnAsignacionMesa.Click += new System.EventHandler(this.btnAsignacionMesa_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Fecha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 145);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Localidad:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.Controls.Add(this.btnAsignacionMesa);
            this.panel1.Location = new System.Drawing.Point(16, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 268);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(531, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Número de mesa asignado:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(531, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Número de votantes asignados:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(531, 142);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Lista de candidatos:";
            // 
            // lstCandidatos
            // 
            this.lstCandidatos.FormattingEnabled = true;
            this.lstCandidatos.ItemHeight = 16;
            this.lstCandidatos.Location = new System.Drawing.Point(676, 142);
            this.lstCandidatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstCandidatos.Name = "lstCandidatos";
            this.lstCandidatos.Size = new System.Drawing.Size(207, 132);
            this.lstCandidatos.TabIndex = 10;
            // 
            // txtNumeroMesa
            // 
            this.txtNumeroMesa.Enabled = false;
            this.txtNumeroMesa.Location = new System.Drawing.Point(748, 54);
            this.txtNumeroMesa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNumeroMesa.Name = "txtNumeroMesa";
            this.txtNumeroMesa.Size = new System.Drawing.Size(75, 22);
            this.txtNumeroMesa.TabIndex = 11;
            // 
            // txtVotantesAsignados
            // 
            this.txtVotantesAsignados.Enabled = false;
            this.txtVotantesAsignados.Location = new System.Drawing.Point(748, 92);
            this.txtVotantesAsignados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVotantesAsignados.Name = "txtVotantesAsignados";
            this.txtVotantesAsignados.Size = new System.Drawing.Size(75, 22);
            this.txtVotantesAsignados.TabIndex = 12;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(388, 313);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(152, 47);
            this.btnCerrar.TabIndex = 13;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(970, 54);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Número de mesa asignado:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // tb_mesaAConsulta
            // 
            this.tb_mesaAConsulta.Enabled = false;
            this.tb_mesaAConsulta.Location = new System.Drawing.Point(1246, 48);
            this.tb_mesaAConsulta.Margin = new System.Windows.Forms.Padding(4);
            this.tb_mesaAConsulta.Name = "tb_mesaAConsulta";
            this.tb_mesaAConsulta.Size = new System.Drawing.Size(75, 22);
            this.tb_mesaAConsulta.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1053, 19);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Consulta de mesa";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(970, 98);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Localidad:";
            // 
            // cb_LocalidadConsulta
            // 
            this.cb_LocalidadConsulta.FormattingEnabled = true;
            this.cb_LocalidadConsulta.Location = new System.Drawing.Point(1056, 98);
            this.cb_LocalidadConsulta.Margin = new System.Windows.Forms.Padding(4);
            this.cb_LocalidadConsulta.Name = "cb_LocalidadConsulta";
            this.cb_LocalidadConsulta.Size = new System.Drawing.Size(265, 24);
            this.cb_LocalidadConsulta.TabIndex = 18;
            this.cb_LocalidadConsulta.SelectedIndexChanged += new System.EventHandler(this.cb_LocalidadConsulta_SelectedIndexChanged);
            // 
            // tb_ConsultaMesa
            // 
            this.tb_ConsultaMesa.Enabled = false;
            this.tb_ConsultaMesa.Location = new System.Drawing.Point(946, 252);
            this.tb_ConsultaMesa.Margin = new System.Windows.Forms.Padding(4);
            this.tb_ConsultaMesa.Name = "tb_ConsultaMesa";
            this.tb_ConsultaMesa.Size = new System.Drawing.Size(348, 22);
            this.tb_ConsultaMesa.TabIndex = 19;
            // 
            // btn_Consulta
            // 
            this.btn_Consulta.Location = new System.Drawing.Point(1082, 145);
            this.btn_Consulta.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Consulta.Name = "btn_Consulta";
            this.btn_Consulta.Size = new System.Drawing.Size(152, 47);
            this.btn_Consulta.TabIndex = 4;
            this.btn_Consulta.Text = "Consulta";
            this.btn_Consulta.UseVisualStyleBackColor = true;
            // 
            // AsignacionMesaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 499);
            this.Controls.Add(this.btn_Consulta);
            this.Controls.Add(this.tb_ConsultaMesa);
            this.Controls.Add(this.cb_LocalidadConsulta);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_mesaAConsulta);
            this.Controls.Add(this.label6);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_mesaAConsulta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_LocalidadConsulta;
        private System.Windows.Forms.TextBox tb_ConsultaMesa;
        private System.Windows.Forms.Button btn_Consulta;
    }
}