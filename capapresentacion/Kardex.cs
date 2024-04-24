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
    public partial class Kardex : Form
    {
        public bool IsNuevo = false;
        private bool IsEditar = false;

        private void Limpiar()
        {
            //this.txtid.Text = string.Empty;
           // this.txtnombresoli.Text = string.Empty;
            //this.txtproveedor.Text = string.Empty;
            this.textboxprod.Text = string.Empty;
            this.textboxdescri.Text = string.Empty;
            this.textBoxcantr.Text = string.Empty;
            //this.textBoxfech.Text = string.Empty;
            //this.textBoxoc.Text = string.Empty;
            this.textBoxpunitario.Text = string.Empty;
            //this.textBoxobser.Text = string.Empty;

        }
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema CAINE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema CAINE ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private string cantidad_inicial;
        private string cantidad_actual;
        private string idproductob;

        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            // this.txtid.ReadOnly = !valor;
            //this.txtproveedor.ReadOnly = !valor;
            this.textboxprod.ReadOnly = !valor;
            this.textboxdescri.ReadOnly = !valor;
            this.textBoxcantr.ReadOnly = !valor;
            // this.textBoxfech.ReadOnly = !valor;
            //this.txtnombresoli.ReadOnly = !valor;
            //this.textBoxpunitario.ReadOnly = !valor;
            //this.textBoxobser.ReadOnly = !valor;
            //this.textBoxoc.ReadOnly = !valor;

        }
        //Habilitar los botones
        private void Botones()
        {
            if (this.IsNuevo || this.IsEditar) //Alt + 124
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = false;
            }
        }

        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
           this.dataListado.Columns[2].Visible = false;
            //this.dataListado.Columns[3].Visible = false;
            //this.dataListado.Columns[4].Visible = false;

        }
        private static Kardex _Instancia;
        public static Kardex GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new Kardex();
            }
            return _Instancia;
        }

        public void setproducto(string idproductob, string producto, string descripcion, string cant, string punitario, string actual)
        {
            this.textboxprod.Text = producto;
            this.textboxdescri.Text = descripcion;
            this.textBoxpunitario.Text = punitario;
            //this.dtFecha_Vencimiento.Text = Convert.ToString(fvencimiento);
            this.idproductob = idproductob;
            this.cantidad_inicial = cant;
            this.cantidad_actual = actual;
            this.textboxpendiente.Text = actual;
        }

        private void Mostrar()
        {
            this.dataListado.DataSource = Nkardex.Mostrar();
            this.OcultarColumnas();
            this.dataListado.Columns["idkardex"].Visible = false;
            this.dataListado.Columns["idproductosb"].Visible = false;
            this.dataListado.Columns["iddetallesolicitud"].Visible = false;
            lblTotal.Text = "Total de Productos visibles: " + Convert.ToString(dataListado.Rows.Count);
        }
        public Kardex()
        {
            InitializeComponent();
        }

        private void Kardex_Load(object sender, EventArgs e)
        {
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //this.txtid.Text == string.Empty 
                string rpta = "";
                if (this.textboxprod.Text == string.Empty || this.textboxdescri.Text == string.Empty || this.textBoxcantr.Text == string.Empty || this.textBoxpunitario.Text == string.Empty
                    || this.textBoxentrega.Text == string.Empty || this.textBoxrecibe.Text == string.Empty)
                {
                    MensajeError("Falta ingresar datos, serán remarcados");
                    erroricono.SetError(textboxprod, "Ingrese un Valor");
                    erroricono.SetError(textBoxentrega, "Ingrese un valor");
                    erroricono.SetError(textBoxrecibe,"Ingrese un valor");
                    erroricono.SetError(textboxdescri, "Ingrese un Valor");
                    erroricono.SetError(textBoxcantr, "Ingrese un valor");
                    //Kardex text = new Kardex();
                    //foreach (TextBox t in text){
                    //}
                    // investigar como recorrer todos los textbox de un formulario

                }
                else
                {
                    int total,restante;
                    total=Convert.ToInt32(textBoxpunitario.Text)*Convert.ToInt32(textBoxcantr.Text);
                    restante=Convert.ToInt32(this.cantidad_actual)-Convert.ToInt32(textBoxcantr.Text);
                    if (restante >= 0)
                    {
                        if (this.IsNuevo)
                        {
                            rpta = Nkardex.insertar(Convert.ToInt32(this.idproductob), this.dtFecha.Value, this.textboxprod.Text, this.textboxdescri.Text,
                                Convert.ToInt32(this.textBoxcantr.Text), Convert.ToInt32(this.textBoxpunitario.Text), total, restante, this.textBoxentrega.Text, this.textBoxrecibe.Text);
                        }
                        else
                        {
                            //rpta = Norden.Editar(Convert.ToInt32(this.txtid.Text), this.txtproveedor.Text, this.textBoxdirec.Text,
                            //    this.textBoxrtn.Text.Trim(), this.dtFechao.Value, this.textBoxtel.Text,
                            //   this.textBoxnrepr.Text, this.textBoxoc.Text, "En proceso");
                        }

                        if (rpta.Equals("OK"))
                        {
                            if (this.IsNuevo)
                            {
                                this.MensajeOk("Operación realizada correctamente");
                            }
                            else
                            {
                                this.MensajeOk("Se Actualizó de forma correcta");
                            }
                        }
                        else
                        {
                            this.MensajeError(rpta);
                        }
                        this.IsNuevo = false;
                        this.IsEditar = false;
                        this.Botones();
                        this.Limpiar();
                        this.Mostrar();
                        tabControl1.SelectTab(0);
                    }
                    else {

                        this.MensajeError("Error, Cantidad en bodega insuficiente");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdkardex.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Debe de seleccionar primero el registro a Modificar");
            }

        }
        private void btnBuscarproducto_Click(object sender, EventArgs e)
        {
            vistaproductobod n = new vistaproductobod();
            n.ShowDialog();
        }

        private void Kardex_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Instancia = null;
        }
        private void BuscarProducto()
        {
            this.dataListado.DataSource = Nkardex.Buscarproducto(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarProducto();
        }

        //Método BuscarFechas
        private void BuscarFechas()
        {
            this.dataListado.DataSource = Nkardex.BuscarFechas(this.dtFecha1.Value.ToString("dd/MM/yyyy"),
                this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void vistaBuscarFechas()
        {
            this.dataListado.DataSource = Nkardex.vistaBuscarFechas(this.dtFecha1.Value.ToString("dd/MM/yyyy"),
                this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            vistaBuscarFechas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //esta variable y ocndicional es para revisar que haya contenido en el datagriev y ver si esta seleccinado el chekbox eliminar
            int producto;
            producto = dataListado.Rows.Count;
            if (producto <= 0 || (chkEliminar.Checked == false)) { MensajeError("No hay registro para eliminar"); }
            else
            {
                try
                {
                    DialogResult Opcion;
                    Opcion = MessageBox.Show("Realmente Desea Eliminar la salida del producto", "Sistema CAINE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (Opcion == DialogResult.OK)
                    {
                        string Codigo;
                        string Rpta = "";

                        foreach (DataGridViewRow row in dataListado.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells[0].Value))
                            {
                                Codigo = Convert.ToString(row.Cells["idkardex"].Value);
                                Rpta = Nkardex.Eliminar(Convert.ToInt32(Codigo));

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

        private void button1_Click(object sender, EventArgs e)
        {
            //este es el boto para imprimir
            try
            {
                Reportekardex imp = new Reportekardex();
                imp.Texto = dtFecha1.Value.ToString("dd/MM/yyyy");
                imp.Texto2 = dtFecha2.Value.ToString("dd/MM/yyyy");
                imp.ShowDialog();
            }
            catch
            {
                this.MensajeError("Seleccione un lapso de fechas valido");

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

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }
    }
}
