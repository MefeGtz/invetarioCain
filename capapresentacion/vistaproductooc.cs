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
    public partial class vistaproductooc : Form
    {
        private void OcultarColumnasproductooc()
        {
            //para la orden de compra
            this.dataListadoproductooc.Columns[0].Visible = false;
            this.dataListadoproductooc.Columns[2].Visible = false;
            //this.dataListadoproductooc.Columns[1].Visible = false;
            //this.dataListadoproductooc.Columns[4].Visible = false;
        }
        private void MostrarDetalle()
        {
            //ese id es de la orden de compra
            this.dataListadoproductooc.DataSource = Norden.MostrarDetalle1(Class1.idordenc);
            OcultarColumnasproductooc();
        }

        private void buscarproducto()
        {
            //ese id es de la orden de compra
            this.dataListadoproductooc.DataSource = Norden.buscarproductooc(Class1.idordenc,txtBuscar.Text);
            dataListadoproductooc.Columns["idproductooc"].Visible = false;
        }
        public vistaproductooc()
        {
            InitializeComponent();
        }

        private void vistaproductooc_Load(object sender, EventArgs e)
        {
            labelorden.Text = Class1.ordenc;
            MostrarDetalle();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarproducto();

        }

        private void dataListadoproductooc_DoubleClick(object sender, EventArgs e)
        {
            string idproductooc, nombreproducto, descripcion, puniario, cantidadinicial, pendientesol;
            DateTime fvencimiento;
            idproductooc = Convert.ToString(this.dataListadoproductooc.CurrentRow.Cells["idproductooc"].Value);
            nombreproducto = Convert.ToString(this.dataListadoproductooc.CurrentRow.Cells["Producto"].Value);
            descripcion = Convert.ToString(this.dataListadoproductooc.CurrentRow.Cells["Descripción"].Value);
            puniario = Convert.ToString(this.dataListadoproductooc.CurrentRow.Cells["P_unitario"].Value);
            cantidadinicial = Convert.ToString(this.dataListadoproductooc.CurrentRow.Cells["Cantidad"].Value);
            fvencimiento =Convert.ToDateTime(this.dataListadoproductooc.CurrentRow.Cells["F_vencimiento"].Value);
            pendientesol = Convert.ToString(this.dataListadoproductooc.CurrentRow.Cells["Pendiente_sol"].Value);


            Solicitudes s = Solicitudes.GetInstancia();
            s.setproducto(idproductooc, nombreproducto, descripcion, fvencimiento, cantidadinicial, puniario,pendientesol);
            this.Close();

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscarproducto();
        }
    }
}
