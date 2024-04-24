using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace capapresentacion
{
    public partial class imprimirsolicitud : Form
    {
        public imprimirsolicitud()
        {
            InitializeComponent();
        }

        private String _Texto;

        public String Texto
        {
            get { return _Texto; }
            set { _Texto = value; }
        }

        private void imprimirsolicitud_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetprincipal.spimprimirsolicitud' Puede moverla o quitarla según sea necesario.
            this.spimprimirsolicitudTableAdapter.Fill(this.DataSetprincipal.spimprimirsolicitud,Convert.ToInt32(Texto));
            this.reportViewer1.RefreshReport();
        }
    }
}
