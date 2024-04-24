using System;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
//using System.Data;
//using DevComponents.DotNetBar;
using capanegocio;

namespace capapresentacion
{
    public partial class formordenc : Form
    {
        private DataTable dtproductooc;
        // NOS SERVIRA PARA ver si la operacion es editar o es nuevo.
        private bool IsNuevo = false;
        private bool IsEditar = false;
        // para sumar el total de los productos agregados
        private int totalPagado = 0;

        //Mostrar Mensaje de Confirmación
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema CAINE ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema CAINE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            this.txtid.Text = string.Empty;
            this.txtproveedor.Text = string.Empty;
            this.textBoxtel.Text = string.Empty;
            this.textBoxrtn.Text = string.Empty;
            //this.textBoxfech.Text = string.Empty;
            this.textBoxoc.Text = string.Empty;
            this.textBoxnrepr.Text = string.Empty;
            this.textBoxdirec.Text = string.Empty;
            Mostrar();
       
        }

        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            this.txtid.ReadOnly = !valor;
            this.txtproveedor.ReadOnly = !valor;
            this.textBoxdirec.ReadOnly = !valor;
           // this.textBoxfech.ReadOnly = !valor;
            this.textBoxrtn.ReadOnly = !valor;
            this.textBoxnrepr.ReadOnly = !valor;
            this.textBoxtel.ReadOnly = !valor;
            this.textBoxoc.ReadOnly = !valor;
            this.textboxprod.ReadOnly = !valor;
            this.textboxdescri.ReadOnly = !valor;
            this.textboxcant.ReadOnly = !valor;
            this.textBoxpunitario.ReadOnly = !valor;
            this.textboxIdcat.ReadOnly = !valor;
           
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
                this.btnAgregar.Enabled = true;
                this.btnQuitar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
                this.btnAgregar.Enabled = false;
                this.btnQuitar.Enabled = false;
            }

        }

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            //para la orden de compra
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }
        private void OcultarColumnasproductooc()
        {
            //para la orden de compra
            this.dataListadoproductooc.Columns[0].Visible = false;
            this.dataListadoproductooc.Columns[2].Visible = false;
           // this.dataListadoproductooc.Columns[].Visible = false;
        }

        //para tomaar los datos del otro formulario
        private static formordenc _Instancia;
        public static formordenc GetInstancia()

        {
            if (_Instancia == null)
            {
                _Instancia = new formordenc();
            }
            return _Instancia;
        }
        public void setCategoria(string idcategoria, string nombre)
        {
            this.textboxIdcat.Text = idcategoria;
            this.textboxcat.Text = nombre;
        }

        //Método Mostrar
        private void Mostrartodaorden()
        {
            this.dataListado.DataSource = Norden.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void Mostrar()
        {
            this.dataListado.DataSource = Norden.Mostrarordensinliquidar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void MostrarDetalle()
        {
          //ese id es de la orden de compra
            this.dataListadoproductooc.DataSource = Norden.MostrarDetalle(this.txtid.Text);
            this.dataListadoproductooc.Columns["idproductooc"].Visible = false;

        }

        private void crearTabla()
        {
            this.dataListadoproductooc.Columns.Clear();
            this.dtproductooc = new DataTable("DetalleProductooc");
            this.dtproductooc.Columns.Add("idorden", System.Type.GetType("System.Int32"));
            this.dtproductooc.Columns.Add("Orden", System.Type.GetType("System.String"));
            this.dtproductooc.Columns.Add("idcategoria", System.Type.GetType("System.Int32"));
            this.dtproductooc.Columns.Add("Categoría", System.Type.GetType("System.String"));
            this.dtproductooc.Columns.Add("Producto", System.Type.GetType("System.String"));
            this.dtproductooc.Columns.Add("Descripción", System.Type.GetType("System.String"));
            this.dtproductooc.Columns.Add("fvencimiento", System.Type.GetType("System.DateTime"));
            this.dtproductooc.Columns.Add("Cantidad", System.Type.GetType("System.Int32"));
            this.dtproductooc.Columns.Add("P_unitario", System.Type.GetType("System.Int32"));
            this.dtproductooc.Columns.Add("Total", System.Type.GetType("System.Int32"));
            this.dtproductooc.Columns.Add("Pendiente_sol", System.Type.GetType("System.Int32"));
            //Relacionar nuestro DataGRidView con nuestro DataTable
            this.dataListadoproductooc.DataSource = this.dtproductooc;
            this.dataListadoproductooc.Columns["idorden"].Visible = false;
            this.dataListadoproductooc.Columns["idcategoria"].Visible = false;
            //this.OcultarColumnasproductooc();
        }

        private void Buscarorden()
        {
            this.dataListado.DataSource = Norden.Buscarorden(this.dtFecha1.Value.ToString("dd/MM/yyyy"), this.dtfecha2.Value.ToString("dd/MM/yyyy"));
            //this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        public formordenc()
        {
            InitializeComponent();

        }

        private void formordenc_Load(object sender, EventArgs e)
        {  
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
            this.crearTabla();
            this.textboxcat.ReadOnly = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.limpiarDetalle();
            this.crearTabla();
            this.Habilitar(true);
            this.txtproveedor.Focus();

            //SqlCommand comando = new SqlCommand("SELECT * FROM  ordencompra WHERE idorden = @textobuscar", conexion);
            //comando.Parameters.AddWithValue("@textobuscar", 1);
            //conexion.Open();
            //SqlDataReader registro = comando.ExecuteReader;
            //if (registro.Read()) { 
            //}
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
               // DateTime hoy;this.textboxcat.Text == string.Empty || textboxprod.Text == string.Empty || textboxdescri.Text == string.Empty || textboxcant.Text == string.Empty
                   // || textBoxpunitario.Text == string.Empty
               
                string rpta = "";
                if (this.txtproveedor.Text == string.Empty ||  this.textBoxoc.Text == string.Empty ||
                    this.textBoxrtn.Text == string.Empty || this.textBoxtel.Text == string.Empty || this.textBoxnrepr.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    erroricono.SetError(txtproveedor, "Ingrese un Valor");
                    erroricono.SetError(textBoxoc, "Ingrese un Valor");
                    erroricono.SetError(textBoxrtn, "Ingrese un Valor");
                    erroricono.SetError(textBoxtel, "Ingrese un Valor");
                    erroricono.SetError(textBoxnrepr, "Ingrese un Valor");
                }
                else
                {
                    int productosoc;
                    productosoc = dataListadoproductooc.Rows.Count;
                    if (productosoc <= 0) { MensajeError("No hay productos para la solicitud"); }
                    else
                    {
                        if (this.IsNuevo)
                        {
                            rpta = Norden.Insertar(this.txtproveedor.Text, this.textBoxdirec.Text,
                                this.textBoxrtn.Text.Trim(), dtFechao.Value, this.textBoxtel.Text,
                               this.textBoxnrepr.Text, this.textBoxoc.Text, "En proceso", dtproductooc);
                        }
                        else
                        {
                            rpta = Norden.Editar(Convert.ToInt32(this.txtid.Text), this.txtproveedor.Text, this.textBoxdirec.Text,
                               this.textBoxrtn.Text, this.dtFechao.Value, this.textBoxtel.Text,
                               this.textBoxnrepr.Text, this.textBoxoc.Text, "En proceso");
                        }
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se Insertó de forma correcta La Orden de Compra");
                        }
                        else
                        {
                            this.MensajeOk("Se Actualizó de forma correcta La Orden de compra");
                        }
                        this.IsNuevo = false;
                        this.IsEditar = false;
                        this.Botones();
                        this.Limpiar();
                        this.Mostrar();
                        this.limpiarDetalle();
                        crearTabla();
                        tabControl1.SelectTab(0);
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.dataListadoproductooc.Columns.Clear();
            this.txtid.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idorden"].Value);
            this.txtproveedor.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Proveedor"].Value);
            this.textBoxdirec.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Dirección"].Value);
            this.textBoxrtn.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["RTN"].Value);
            this.dtFechao.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["Fecha"].Value);
            this.textBoxtel.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Teléfono"].Value);
            this.textBoxnrepr.Text= Convert.ToString(this.dataListado.CurrentRow.Cells["Representante"].Value);
            this.textBoxoc.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Orden"].Value);
            this.MostrarDetalle();
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Habilitar(false);
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtid.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
                btnAgregar.Enabled = false;
                btnQuitar.Enabled = false;
                MessageBox.Show("Solo se puede modificar los datos de la orden de compra");
            }
            else
            {
                this.MensajeError("Debe de seleccionar primero una orden de compra para Modificar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            limpiarDetalle();
            this.Habilitar(false);
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int productosoc;
            productosoc = dataListado.Rows.Count;
            if (productosoc <= 0 || (chkEliminar.Checked==false)) { MensajeError("No hay Orden de Compra para eliminar"); }
            else
            {
                try
                {
                    DialogResult Opcion;
                    Opcion = MessageBox.Show("Realmente Desea Eliminar los Registros", "Orden de compra CAINE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (Opcion == DialogResult.OK)
                    {
                        string Codigo;
                        string Rpta = "";

                        foreach (DataGridViewRow row in dataListado.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells[0].Value))
                            {
                                bool existe = true;
                                Codigo = Convert.ToString(row.Cells[1].Value);
                                //este ciclo es para que elimine las solicitudes relacionadas a la orden de compra ya que ya tenemos una relacion en la base de datos de orden a 
                                // los productos de orden, entonces no permite eliminar en cascada ambas llaves foraneas.
                                do
                                {
                                    Rpta = Norden.Eliminarsolicitud(Convert.ToInt32(Codigo));
                                    if (Rpta.Equals("OK"))
                                    {
                                        existe = true;

                                    }
                                    else
                                    {
                                        existe = false;
                                    }
                                } while (existe == true);
                               // MessageBox.Show("se eliminaron las solicitudes");
                                Rpta = Norden.Eliminar(Convert.ToInt32(Codigo));

                                if (Rpta.Equals("OK"))
                                {
                                    this.MensajeOk("Se Eliminó Correctamente La Orden de compra");
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

        private void limpiarDetalle()
        {
            this.textboxprod.Text = string.Empty;
            this.textboxcat.Text = string.Empty;
            this.textboxdescri.Text = string.Empty;
            this.textboxcant.Text = string.Empty;
            this.textBoxpunitario.Text = string.Empty;
            this.textboxIdcat.Clear();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textboxcat.Text == string.Empty || this.textboxcant.Text == string.Empty
                    || this.textBoxpunitario.Text == string.Empty || this.textboxprod.Text == string.Empty || textboxdescri.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    erroricono.SetError(textboxprod, "Ingrese un Valor");
                    erroricono.SetError(textboxdescri, "Ingrese un Valor");
                    erroricono.SetError(textBoxpunitario, "Ingrese un Valor");
                    erroricono.SetError(textboxcat, "Ingrese un Valor");
                    erroricono.SetError(textboxcant, "Ingrese un Valor");
                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow row in dtproductooc.Rows)
                    {
                        if (Convert.ToString(row["Producto"]) == this.textboxprod.Text)
                        {
                            registrar = false;
                            this.MensajeError("Ya se encuentra el producto en la orden de compra");
                        }
                    }
                    if (registrar)
                    {
                        int subTotal = 0;
                        subTotal = Convert.ToInt32(this.textboxcant.Text) * Convert.ToInt32(this.textBoxpunitario.Text);
                        totalPagado = totalPagado + subTotal;
                        this.lblTotal_Pagado.Text = totalPagado.ToString("#0.00#");
                        //Agregar ese detalle al datalistadoDetalle
                        DataRow row = this.dtproductooc.NewRow();
                        //  row["idproductooc"] = Convert.ToInt32(this.txtIdarticulo.Text);, this.txtid.Text;
                        row["idorden"] = 0;
                       row["Orden"] = this.textBoxoc.Text;
                        row["idcategoria"] =this.textboxIdcat.Text;
                        row["Categoría"] = textboxcat.Text;
                        row["Producto"] = textboxprod.Text;
                        row["Descripción"] = textboxdescri.Text;
                        row["fvencimiento"] = dtFecha_Vencimiento.Value;
                        row["Cantidad"] = Convert.ToInt32(this.textboxcant.Text);
                        row["P_unitario"] = Convert.ToInt32(this.textBoxpunitario.Text);
                        row["Total"] = subTotal;
                       row["Pendiente_sol"] = Convert.ToInt32(this.textboxcant.Text);
                        this.dtproductooc.Rows.Add(row);
                        this.limpiarDetalle();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                int indiceFila = this.dataListadoproductooc.CurrentCell.RowIndex;
                DataRow row = this.dtproductooc.Rows[indiceFila];
                //Disminuir el totalPAgado
               totalPagado = totalPagado - (Convert.ToInt32(row["total"].ToString()));
                this.lblTotal_Pagado.Text = totalPagado.ToString("#0.00#");
                //Removemos la fila
                this.dtproductooc.Rows.Remove(row);
            }
            catch (Exception ex)
            {
                MensajeError("No hay fila para remover");
            }
        }
        private void btnBuscarArticulo_Click(object sender, EventArgs e)
        {
            vistacategoria vista = new vistacategoria();
           // vista.Show();
            vista.ShowDialog();   
        }
        private void formordenc_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Instancia = null;
        }

        private void formordenc_Activated(object sender, EventArgs e)
        {
        }

        private void formordenc_Validating(object sender, CancelEventArgs e)
        {
          // this.textboxIdcat.Text = Convert.ToString(Class1.idcategoria);
          //this.textboxcat.Text = Class1.nombrecat;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscarorden();
        }

        private void btnrefrescar_Click(object sender, EventArgs e)
        {
            this.Mostrar();

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxmostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxmostrar.Checked)
            {
                Mostrartodaorden();
            }
            else
            {
                Mostrar();
            }
        }

    }
}
