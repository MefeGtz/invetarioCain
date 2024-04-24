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
    public partial class productospcant : Form
    {
        public productospcant()
        {
            InitializeComponent();
        }
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
            this.dataListado.Columns[2].Visible = false;

        }

        private void Mostrar()
        {
            this.dataListado.DataSource = Nbodega.Mostrarproductoagastarse();
            this.dataListado.Columns["idproductosb"].Visible = false;
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        //Método BuscarNombre
        private void BuscarNombre()
        {
            this.dataListado.DataSource = Nbodega.buscarproductoagastarse(this.txtBuscar.Text);
            //this.OcultarColumnas();
            this.dataListado.Columns["idproductosb"].Visible = false;
            // this.dataListado.Columns["iddetallesoicitud"].Visible = false;
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void productospcant_Load(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarNombre();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarNombre();
        }
    }
}