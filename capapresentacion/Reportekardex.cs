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
    public partial class Reportekardex : Form
    {
        public Reportekardex()
        {
            InitializeComponent();
        }

        private string _texto;

        public string Texto
        {
            get { return _texto; }
            set { _texto = value; }
        }
        private string _texto2;

        public string Texto2
        {
            get { return _texto2; }
            set { _texto2 = value; }
        }

        private void Reportekardex_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetprincipal.spimprimir_kardex' Puede moverla o quitarla según sea necesario.
            this.spimprimir_kardexTableAdapter.Fill(this.DataSetprincipal.spimprimir_kardex,this.Texto,this.Texto2);
            this.reportViewer1.RefreshReport();
        }
    }
}
