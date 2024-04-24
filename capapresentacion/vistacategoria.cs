﻿using System;
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
    public partial class vistacategoria : Form
    {
       // formordenc n = new formordenc();
        public vistacategoria()
        {
            InitializeComponent();
        }
        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }

        //Método Mostrar
        private void Mostrar()
        {
            this.dataListado.DataSource = NCategoria.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        //Método BuscarNombre
        private void BuscarNombre()
        {
            this.dataListado.DataSource = NCategoria.BuscarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void vistacategoria_Load(object sender, EventArgs e)
        {
         this.Mostrar();
        }
       
        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //formordenc form = formordenc.GetInstancia();
            //string par1, par2;
            //par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            //par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            //form.setCategoria(par1, par2)
        }

        private void dataListado_DoubleClick_1(object sender, EventArgs e)
        {
            //Class1.idcategoria = Convert.ToInt32(Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value));
            //Class1.nombrecat = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            formordenc form = formordenc.GetInstancia();
            string par1, par2;
            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
            form.setCategoria(par1, par2);
            this.Hide();
         this.Close();
        }

    
    }
}
