using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscrutinioGrafica
{
    public partial class Escrutinio : Form
    {
        public Escrutinio()
        {
            InitializeComponent();
        }

        private void btnAsignacion_Click(object sender, EventArgs e)
        {
            AsignacionMesaForm asignacionMesa = new AsignacionMesaForm();
            asignacionMesa.ShowDialog();
        }

        private void btnRegistroDatos_Click(object sender, EventArgs e)
        {
            RegistroDatosForm registro = new RegistroDatosForm();
            registro.ShowDialog();
        }

        private void btnCerrarMesa_Click(object sender, EventArgs e)
        {
            CerrarMesaForm cerrar = new CerrarMesaForm();
            cerrar.ShowDialog();
        }
    }
}
