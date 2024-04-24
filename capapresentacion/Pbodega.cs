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
    public partial class Pbodega : Form
    {
        public Pbodega()
        {
            InitializeComponent();
        }
        private bool IsNuevo = false;
        private bool IsEditar = false;

        //Mostrar Mensaje de Confirmación
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema CAINE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema CAINE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Limpiar todos los controles del formulario
        //private void Limpiar()
        //{
        //    this.textboxcanta.Text = string.Empty;
        //    this.textBoxcanti.Text = string.Empty;
        //    this.textboxdescri.Text = string.Empty;
        //    this.textboxidprod.Text = string.Empty;
        //    this.textboxprod.Text = string.Empty;
        //    this.textBoxpunitario.Text = string.Empty;
        //}

        //Habilitar los controles del formulario
        //private void Habilitar(bool valor)
        //{
        //    //solo va a poder modificar el nombre del producto la descripcion y el preciounitario
        //    this.textboxprod.ReadOnly = !valor;
        //    this.textBoxpunitario.ReadOnly = !valor;
        //    this.textboxdescri.ReadOnly = !valor;
        //}

        //Habilitar los botones
        //private void Botones()
        //{
        //    if (this.IsNuevo || this.IsEditar) //Alt + 124
        //    {
        //        this.Habilitar(true);
        //        this.btnNuevo.Enabled = false;
        //        this.btnGuardar.Enabled = true;
        //        this.btnEditar.Enabled = false;
        //        this.btnCancelar.Enabled = true;
        //    }
        //    else
        //    {
        //        this.Habilitar(false);
        //        this.btnNuevo.Enabled = true;
        //        this.btnGuardar.Enabled = false;
        //        this.btnEditar.Enabled = true;
        //        this.btnCancelar.Enabled = false;
        //    }

        //}


        private void Mostrar()
        {
            this.dataListado.DataSource = Nbodega.Mostrar0();
            this.dataListado.Columns["Orden_c"].Visible = false;
            this.dataListado.Columns["Solicitud"].Visible = false;
            this.dataListado.Columns["idproductosb"].Visible = false;
            this.OcultarColumnas();
            lblTotal.Text = "Total de Productos" + Convert.ToString(dataListado.Rows.Count);
        }
        private void Mostrar0()
        {
            this.dataListado.DataSource = Nbodega.Mostrar0();
            this.dataListado.Columns["Orden_c"].Visible = false;
            this.dataListado.Columns["Solicitud"].Visible = false;
            this.dataListado.Columns["idproductosb"].Visible = false;
            this.OcultarColumnas();
            lblTotal.Text = "Total de Productos" + Convert.ToString(dataListado.Rows.Count);
        }



        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            //this.dataListado.Columns[1].Visible = false;
            this.dataListado.Columns[3].Visible = false;
        }
        //Método BuscarNombre
        private void BuscarNombre()
        {
            this.dataListado.DataSource = Nbodega.buscarnombre(this.txtBuscar.Text);

           // this.dataListado.Columns["iddetallesoicitud"].Visible = false;
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void Pbodega_Load(object sender, EventArgs e)
        {
            //this.Habilitar(false);
            //this.Botones();
            this.Mostrar();
            //this.textboxcanta.ReadOnly = true;
            //this.textBoxcanti.ReadOnly = true;
            //this.textboxidprod.ReadOnly = true;
            //btnNuevo.Enabled = false;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void checkBoxmostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxmostrar.Checked)
            {
                this.dataListado.Columns["Orden_c"].Visible = true;
                this.dataListado.Columns["Solicitud"].Visible = true;
            }
            else
            {
                this.dataListado.Columns["Orden_c"].Visible = false;
                this.dataListado.Columns["Solicitud"].Visible = false;
            }
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
            s.setproducto(idproductob, nombreproducto, descripcion, restante, puniario, restante);
            this.Hide();
            s.Show();
            s.tabControl1.SelectTab(1);
            s.btnGuardar.Enabled = true;
            s.btnCancelar.Enabled = true;
            s.btnEditar.Enabled = false;
            s.btnNuevo.Enabled = false;
            s.IsNuevo = true;
            s.textBoxcantr.Focus();
            s.textBoxcantr.ReadOnly=false;

           // textboxidprod.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idproductosb"].Value);
           // textboxprod.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Producto"].Value);
           // textboxdescri.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Descripción"].Value);
           // textBoxpunitario.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["P_unitario"].Value);
           // textboxcanta.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Cantidad_Actual"].Value);
           // textBoxcanti.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Cantidad_inicial"].Value);
           // dtFechav.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["F_vencimiento"].Value);
           //// this.IsNuevo = false;
           // //this.IsEditar = false;
           // //this.Botones();
           // //this.Habilitar(false);
           // this.tabControl1.SelectedIndex = 1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int elementos;
            elementos = dataListado.Rows.Count;
            if (elementos <= 0 || (chkEliminar.Checked == false)) { MensajeError("No hay producto para eliminar"); }
            else
            {
                try
                {
                    DialogResult Opcion;
                    Opcion = MessageBox.Show("Realmente Desea Eliminar los Registros", "Sistema CAINE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (Opcion == DialogResult.OK)
                    {
                        string Codigo;
                        string Rpta = "";

                        foreach (DataGridViewRow row in dataListado.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells[0].Value))
                            {
                                Codigo = Convert.ToString(row.Cells["idproductosb"].Value);
                                Rpta = Nbodega.Eliminar(Convert.ToInt32(Codigo));

                                if (Rpta.Equals("OK"))
                                {
                                    this.MensajeOk("Se Eliminó Correctamente el registro");
                                }
                                else
                                {
                                    this.MensajeError(Rpta);
                                }
                            }
                        }
                        this.Mostrar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //this.IsNuevo = false;
            //this.IsEditar = false;
            //this.Botones();
            //this.Limpiar();
            //this.Habilitar(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //if (!this.textboxidprod.Text.Equals(""))
            //{
            //    this.IsEditar = true;
            //    this.Botones();
            //    this.Habilitar(true);
            //}
            //else
            //{
            //    this.MensajeError("Debe de seleccionar primero el producto a Modificar");
            //}
        }
    }
}
