using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using capanegocio;

namespace capapresentacion
{
    public partial class vistaordenc : Form
    {
        public vistaordenc()
        {
            InitializeComponent();
        }
        private void OcultarColumnas()
        {
            //para la orden de compra
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }
        private void Mostrar()
        {
            this.dataListado.DataSource = Norden.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void Buscarorden()
        {
            this.dataListado.DataSource = Norden.Buscarorden(this.dtFecha1.Value.ToString("dd/MM/yyyy"), this.dtfecha2.Value.ToString("dd/MM/yyyy"));
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void vistaordenc_Load(object sender, EventArgs e)
        {
            this.Mostrar();
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            string idorden, proveedor,ordencompra;
            idorden = Convert.ToString(this.dataListado.CurrentRow.Cells["idorden"].Value);
            proveedor = Convert.ToString(this.dataListado.CurrentRow.Cells["Proveedor"].Value);
           ordencompra = Convert.ToString(this.dataListado.CurrentRow.Cells["Orden"].Value);

           Solicitudes s = Solicitudes.GetInstancia();
           s.setorden(idorden, proveedor,ordencompra);
           this.Hide();
           s.Show();
           this.Close();

        }
    }
}
