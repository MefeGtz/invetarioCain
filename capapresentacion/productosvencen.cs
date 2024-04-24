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
    public partial class productosvencen : Form
    {
        public productosvencen()
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
            DateTime hoy = DateTime.Today;
            DateTime fin = hoy.AddDays(7);
            this.dtFecha1.Value = hoy;
            this.dtFecha2.Value = fin;
            this.dataListado.DataSource = Nbodega.buscarfechas(this.dtFecha1.Value.ToString("dd/MM/yyyy"),
                this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.dataListado.Columns["idproductosb"].Visible = false;
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        //Método BuscarNombre
        private void BuscarNombre()
        {
            this.dataListado.DataSource = Nbodega.buscarfechas(this.dtFecha1.Value.ToString("dd/MM/yyyy"),
                this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.dataListado.Columns["idproductosb"].Visible = false;
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
           
        }

        private void productosvencen_Load(object sender, EventArgs e)
        {
            this.Mostrar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
           // this.BuscarNombre();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnrefrescar_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

    }
}
