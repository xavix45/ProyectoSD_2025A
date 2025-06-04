namespace EscrutinioGrafica
{
    partial class RegistroDatosForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLocalidad = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCandidato = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVotantes = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudVotosValidos = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudVotosNulos = new System.Windows.Forms.NumericUpDown();
            this.nudVotosBlancos = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnVaciar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.cmbNumeroMesa = new System.Windows.Forms.ComboBox();
            this.nudAusentes = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudVotosValidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVotosNulos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVotosBlancos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAusentes)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Localidad:";
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(106, 29);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(230, 21);
            this.cmbLocalidad.TabIndex = 6;
            this.cmbLocalidad.SelectedIndexChanged += new System.EventHandler(this.cmbLocalidad_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Número de mesa:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Candidato:";
            // 
            // cmbCandidato
            // 
            this.cmbCandidato.FormattingEnabled = true;
            this.cmbCandidato.Location = new System.Drawing.Point(106, 128);
            this.cmbCandidato.Name = "cmbCandidato";
            this.cmbCandidato.Size = new System.Drawing.Size(230, 21);
            this.cmbCandidato.TabIndex = 11;
            this.cmbCandidato.SelectedIndexChanged += new System.EventHandler(this.cmbCandidato_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Número de electores por mesa:";
            // 
            // txtVotantes
            // 
            this.txtVotantes.AutoSize = true;
            this.txtVotantes.Location = new System.Drawing.Point(294, 100);
            this.txtVotantes.Name = "txtVotantes";
            this.txtVotantes.Size = new System.Drawing.Size(42, 13);
            this.txtVotantes.TabIndex = 13;
            this.txtVotantes.Text = "numero";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Votos Validos:";
            // 
            // nudVotosValidos
            // 
            this.nudVotosValidos.Location = new System.Drawing.Point(264, 163);
            this.nudVotosValidos.Name = "nudVotosValidos";
            this.nudVotosValidos.Size = new System.Drawing.Size(72, 20);
            this.nudVotosValidos.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Votos Nulos:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 243);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Votos Blancos:";
            // 
            // nudVotosNulos
            // 
            this.nudVotosNulos.Location = new System.Drawing.Point(264, 204);
            this.nudVotosNulos.Name = "nudVotosNulos";
            this.nudVotosNulos.Size = new System.Drawing.Size(72, 20);
            this.nudVotosNulos.TabIndex = 18;
            // 
            // nudVotosBlancos
            // 
            this.nudVotosBlancos.Location = new System.Drawing.Point(264, 243);
            this.nudVotosBlancos.Name = "nudVotosBlancos";
            this.nudVotosBlancos.Size = new System.Drawing.Size(72, 20);
            this.nudVotosBlancos.TabIndex = 19;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(36, 319);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 20;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnVaciar
            // 
            this.btnVaciar.Location = new System.Drawing.Point(152, 319);
            this.btnVaciar.Name = "btnVaciar";
            this.btnVaciar.Size = new System.Drawing.Size(75, 23);
            this.btnVaciar.TabIndex = 21;
            this.btnVaciar.Text = "Vaciar";
            this.btnVaciar.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(260, 319);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 22;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // cmbNumeroMesa
            // 
            this.cmbNumeroMesa.FormattingEnabled = true;
            this.cmbNumeroMesa.Location = new System.Drawing.Point(282, 69);
            this.cmbNumeroMesa.Name = "cmbNumeroMesa";
            this.cmbNumeroMesa.Size = new System.Drawing.Size(53, 21);
            this.cmbNumeroMesa.TabIndex = 23;
            this.cmbNumeroMesa.SelectedIndexChanged += new System.EventHandler(this.cmbNumeroMesa_SelectedIndexChanged);
            // 
            // nudAusentes
            // 
            this.nudAusentes.Location = new System.Drawing.Point(264, 278);
            this.nudAusentes.Name = "nudAusentes";
            this.nudAusentes.Size = new System.Drawing.Size(72, 20);
            this.nudAusentes.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Ausentes:";
            // 
            // RegistroDatosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 365);
            this.Controls.Add(this.nudAusentes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbNumeroMesa);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnVaciar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.nudVotosBlancos);
            this.Controls.Add(this.nudVotosNulos);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudVotosValidos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtVotantes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbCandidato);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLocalidad);
            this.Name = "RegistroDatosForm";
            this.Text = "RegistroDatos";
            this.Load += new System.EventHandler(this.RegistroDatosForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.nudVotosValidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVotosNulos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVotosBlancos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAusentes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLocalidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCandidato;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label txtVotantes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudVotosValidos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudVotosNulos;
        private System.Windows.Forms.NumericUpDown nudVotosBlancos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVaciar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.ComboBox cmbNumeroMesa;
        private System.Windows.Forms.NumericUpDown nudAusentes;
        private System.Windows.Forms.Label label5;
    }
}