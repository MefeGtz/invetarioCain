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
    public partial class vistaproductobod : Form
    {
        public vistaproductobod()
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
            this.dataListado.DataSource = Nbodega.Mostrar0();
            this.dataListado.Columns["idproductosb"].Visible = false;
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        //Método BuscarNombre
        private void BuscarNombre()
        {
            this.dataListado.DataSource = Nbodega.buscarnombre(this.txtBuscar.Text);
            //this.OcultarColumnas();
            this.dataListado.Columns["idproductosb"].Visible = false;
           // this.dataListado.Columns["iddetallesoicitud"].Visible = false;
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void vistaproductobod_Load(object sender, EventArgs e)
        {
            this.Mostrar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            string idproductob, nombreproducto, descripcion, puniario, restante;
            //DateTime fvencimiento;
            idproductob = Convert.ToString(this.dataListado.CurrentRow.Cells["idproductosb"].Value);
            nombreproducto = Convert.ToString(this.dataListado.CurrentRow.Cells["Producto"].Value);
            descripcion = Convert.ToString(this.dataListado.CurrentRow.Cells["Descripción"].Value);
            puniario = Convert.ToString(this.dataListado.CurrentRow.Cells["P_unitario"].Value);
            restante = Convert.ToString(this.dataListado.CurrentRow.Cells["Cantidad_Actual"].Value);
            //fvencimiento = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["fvencimiento"].Value);
           // pendientesol = Convert.ToString(this.dataListado.CurrentRow.Cells["pendiente_sol"].Value);
            Kardex s = Kardex.GetInstancia();
            s.setproducto(idproductob, nombreproducto, descripcion,restante, puniario, restante);
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }
    }
}
