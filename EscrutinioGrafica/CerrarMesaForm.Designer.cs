namespace EscrutinioGrafica
{
    partial class CerrarMesaForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLocalidad = new System.Windows.Forms.ComboBox();
            this.btnCerrarMesa = new System.Windows.Forms.Button();
            this.btnCerrarVentana = new System.Windows.Forms.Button();
            this.cmbNumeroMesa = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Número de mesa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Localidad:";
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Items.AddRange(new object[] {
            "Pichincha, Quito",
            "Guayas, Guayaquil",
            "Azuay, Cuenca",
            "Loja, Loja",
            "Imbabura, Ibarra",
            "Cotopaxi, Cotopaxi",
            "Tunguragua, Ambato",
            "Manabí, Portoviejo",
            "Esmeraldas, Esmeraldas",
            "Los Ríos, Babahoyo"});
            this.cmbLocalidad.Location = new System.Drawing.Point(93, 22);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(230, 21);
            this.cmbLocalidad.TabIndex = 10;
            this.cmbLocalidad.SelectedIndexChanged += new System.EventHandler(this.cmbLocalidad_SelectedIndexChanged);
            // 
            // btnCerrarMesa
            // 
            this.btnCerrarMesa.Location = new System.Drawing.Point(24, 119);
            this.btnCerrarMesa.Name = "btnCerrarMesa";
            this.btnCerrarMesa.Size = new System.Drawing.Size(75, 23);
            this.btnCerrarMesa.TabIndex = 14;
            this.btnCerrarMesa.Text = "Cerrar mesa";
            this.btnCerrarMesa.UseVisualStyleBackColor = true;
            this.btnCerrarMesa.Click += new System.EventHandler(this.btnCerrarMesa_Click);
            // 
            // btnCerrarVentana
            // 
            this.btnCerrarVentana.Location = new System.Drawing.Point(232, 119);
            this.btnCerrarVentana.Name = "btnCerrarVentana";
            this.btnCerrarVentana.Size = new System.Drawing.Size(91, 23);
            this.btnCerrarVentana.TabIndex = 15;
            this.btnCerrarVentana.Text = "Cancelar";
            this.btnCerrarVentana.UseVisualStyleBackColor = true;
            this.btnCerrarVentana.Click += new System.EventHandler(this.btnCerrarVentana_Click);
            // 
            // cmbNumeroMesa
            // 
            this.cmbNumeroMesa.FormattingEnabled = true;
            this.cmbNumeroMesa.Location = new System.Drawing.Point(272, 62);
            this.cmbNumeroMesa.Name = "cmbNumeroMesa";
            this.cmbNumeroMesa.Size = new System.Drawing.Size(51, 21);
            this.cmbNumeroMesa.TabIndex = 16;
            // 
            // CerrarMesaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 162);
            this.Controls.Add(this.cmbNumeroMesa);
            this.Controls.Add(this.btnCerrarVentana);
            this.Controls.Add(this.btnCerrarMesa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLocalidad);
            this.Name = "CerrarMesaForm";
            this.Text = "Cerrar Mesa";
            this.Load += new System.EventHandler(this.CerrarMesaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLocalidad;
        private System.Windows.Forms.Button btnCerrarMesa;
        private System.Windows.Forms.Button btnCerrarVentana;
        private System.Windows.Forms.ComboBox cmbNumeroMesa;
    }
}